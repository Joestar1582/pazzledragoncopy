using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class OrigamiCutter{
	
	// 左右判定.
	public static bool GetSide ( Vector3 CutVec, Vector3 PointVec ){
		if( (CutVec.z * PointVec.x - CutVec.x * PointVec.z) < 0 ){
			return true;
		}
		return false;
	}
	
	public static bool Cut ( GameObject OrigamiObj, Vector3 HitPoint1, Vector3 HitPoint2 ){
		
		Vector3	LocalHitPoint1 = OrigamiObj.transform.InverseTransformPoint( HitPoint1 );
		Vector3	LocalHitPoint2 = OrigamiObj.transform.InverseTransformPoint( HitPoint2 );
		Vector3	LocalHitVec = LocalHitPoint2 - LocalHitPoint1;
		Vector3	LocalNormal = Vector3.Cross(LocalHitVec.normalized, new Vector3(0,1,0));
		
		Mesh 		OrigamiMesh = OrigamiObj.GetComponent<MeshFilter>().mesh;
		Vector3[] 	Vertices = OrigamiMesh.vertices;
		int[]		Index = OrigamiMesh.triangles;
		int[]		VertCnt = new int[2]{ 0, 0 };
		int[]		TriangleCnt = new int[2];
		Vector3		CrossPoint = Vector3.zero;
		bool		OriFlg = true;
		// 頂点数カウント.
		for ( int i = 0; i < Index.Length; i+=3 ){
			TriangleCnt[0] = 0;
			TriangleCnt[1] = 0;
			for( int j = 0; j < 3; j++ ){
				if( GetSide( LocalHitVec, Vertices[Index[i+j]] - LocalHitPoint1 ) ){
					TriangleCnt[0]++;
					if( TriangleCnt[0] == 3 ){
						TriangleCnt[0] = 1;
					}
				}
				else{
					TriangleCnt[1]++;
					if( TriangleCnt[1] == 3 ){
						TriangleCnt[1] = 1;
					}
				}
			}
			VertCnt[0] += 3 * TriangleCnt[0];
			VertCnt[1] += 3 * TriangleCnt[1];
		}
		
		// 分割の必要なし.
		if( VertCnt[0] == 0 || VertCnt[1] == 0 ) {
			return false;
		}
		
		// 頂点作成.
		Vector3[]	Vertex1 = new Vector3[VertCnt[0]];
		int[]		Index1	= new int[VertCnt[0]];
		Vector3[]	Vertex2 = new Vector3[VertCnt[1]];
		int[]		Index2	= new int[VertCnt[1]];
		int			OneVertIndex = 0;
		int[]		TwoVertIndex = new int[2]{ 0,0 };
		Vector3[]	VertCrossPoint = new Vector3[2];
		VertCnt[0] = 0;
		VertCnt[1] = 0;
		for ( int i = 0; i < Index.Length; i+=3 ){
			TriangleCnt[0] = 0;
			TriangleCnt[1] = 0;
			for( int j = 0; j < 3; j++ ){
				if( GetSide( LocalHitVec, Vertices[Index[i+j]] - LocalHitPoint1 ) ){
					TriangleCnt[0]++;
				}
				else{
					TriangleCnt[1]++;
				}
			}
			
			if( TriangleCnt[0] != 0 && TriangleCnt[1] != 0 ){
				int	Cnt = 0;
				for( int j = 0; j < 3; j++ ){
					if( GetSide( LocalHitVec, Vertices[Index[i+j]] - LocalHitPoint1 ) ){
						if( TriangleCnt[0] == 2 ){
							TwoVertIndex[Cnt] = Index[i+j];
							Cnt++;
						}
						else{
							OneVertIndex = Index[i+j];
						}
					}
					else{
						if( TriangleCnt[1] == 2 ){
							TwoVertIndex[Cnt] = Index[i+j];
							Cnt++;
						}
						else{
							OneVertIndex = Index[i+j];
						}
					}
				}
				
				StaticMath.RayCrossPoint( Vertices[TwoVertIndex[0]], Vertices[OneVertIndex], LocalHitPoint1-LocalHitVec, LocalHitPoint2+LocalHitVec, ref VertCrossPoint[0] );
				StaticMath.RayCrossPoint( Vertices[TwoVertIndex[1]], Vertices[OneVertIndex], LocalHitPoint1-LocalHitVec, LocalHitPoint2+LocalHitVec, ref VertCrossPoint[1] );
				
				if( TriangleCnt[0] == 2 ){
					Vertex1[VertCnt[0]+0] = Vertices[TwoVertIndex[0]];
					Vertex1[VertCnt[0]+1] = VertCrossPoint[0];
					Vertex1[VertCnt[0]+2] = VertCrossPoint[1];
					Vertex1[VertCnt[0]+3] = Vertices[TwoVertIndex[0]];
					Vertex1[VertCnt[0]+4] = VertCrossPoint[1];
					Vertex1[VertCnt[0]+5] = Vertices[TwoVertIndex[1]];
					for( int j = 0; j < 6; j++ )	Index1[Index1.Length-VertCnt[0]-j-1] = VertCnt[0]+j;
					VertCnt[0] += 6;
					Vertex2[VertCnt[1]+0] = VertCrossPoint[0];
					Vertex2[VertCnt[1]+1] = Vertices[OneVertIndex];
					Vertex2[VertCnt[1]+2] = VertCrossPoint[1];
					for( int j = 0; j < 3; j++ )	Index2[VertCnt[1]+j] = VertCnt[1]+j;
					VertCnt[1] += 3;
				}
				else{
					Vertex2[VertCnt[1]+0] = Vertices[TwoVertIndex[0]];
					Vertex2[VertCnt[1]+1] = VertCrossPoint[0];
					Vertex2[VertCnt[1]+2] = VertCrossPoint[1];
					Vertex2[VertCnt[1]+3] = Vertices[TwoVertIndex[0]];
					Vertex2[VertCnt[1]+4] = VertCrossPoint[1];
					Vertex2[VertCnt[1]+5] = Vertices[TwoVertIndex[1]];
					for( int j = 0; j < 6; j++ )	Index2[VertCnt[1]+j] = VertCnt[1]+j;
					VertCnt[1] += 6;
					Vertex1[VertCnt[0]+0] = VertCrossPoint[0];
					Vertex1[VertCnt[0]+1] = Vertices[OneVertIndex];
					Vertex1[VertCnt[0]+2] = VertCrossPoint[1];
					for( int j = 0; j < 3; j++ )	Index1[Index1.Length-VertCnt[0]-j-1] = VertCnt[0]+j;
					VertCnt[0] += 3;
				}
			}
			else{
				if( TriangleCnt[0] == 0 ){
					for( int j = 0; j < 3; j++ ){
						Vertex2[VertCnt[1]] = Vertices[Index[i+j]];
						Index2[VertCnt[1]] = VertCnt[1];
						VertCnt[1]++;
					}
				}
				else if( TriangleCnt[1] == 0 ){
					for( int j = 0; j < 3; j++ ){
						Vertex1[VertCnt[0]] = Vertices[Index[i+j]];
						Index1[Index1.Length-VertCnt[0]-1] = VertCnt[0];
						VertCnt[0]++;
					}
				}
			}
		}
		
		
		// 最適化.
		List<Vector3>	VertList = new List<Vector3>();
		int[]	OptimizeIndex = new int[Index1.Length+Index2.Length];
		for( int i = 0; i < Index1.Length; i++ ){
			bool Hit = false;
			for( int j = 0; j < VertList.Count; j++ ){
				if( VertList[j] == Vertex1[Index1[i]]){
					Hit = true;
					OptimizeIndex[i] = j;
					break;
				}
			}
			if( !Hit ){
				VertList.Add( Vertex1[Index1[i]] );
				OptimizeIndex[i] = VertList.Count-1;
			}
		}
		for( int i = 0; i < Index2.Length; i++ ){
			bool Hit = false;
			for( int j = 0; j < VertList.Count; j++ ){
				if( VertList[j] == Vertex2[Index2[i]]){
					Hit = true;
					OptimizeIndex[Index1.Length+i] = j;
					break;
				}
			}
			if( !Hit ){
				VertList.Add( Vertex2[Index2[i]] );
				OptimizeIndex[Index1.Length+i] = VertList.Count-1;
			}
		}
		Vector3[]	OptimizeVertex = new Vector3[VertList.Count];
		for( int i = 0; i < VertList.Count; i++ ){
			OptimizeVertex[i] = VertList[i];
		}
		
		
		// メッシュ作成.
		Mesh NewMesh = new Mesh();
		Vector2[]	UVs = new Vector2[OptimizeVertex.Length];
		NewMesh.vertices = OptimizeVertex;
		NewMesh.triangles = OptimizeIndex;
		NewMesh.uv = UVs;
		NewMesh.RecalculateBounds();
		OrigamiObj.GetComponent<MeshFilter>().mesh = NewMesh;
		OrigamiObj.GetComponent<MeshCollider>().sharedMesh = NewMesh;
		//other.GetComponent<BoxCollider>().size = NewMesh.bounds.size;
		//other.GetComponent<BoxCollider>().center = NewMesh.bounds.center;
		
		OrigamiCollider origamiCollider = OrigamiObj.GetComponent<OrigamiCollider>();
		OriFlg = origamiCollider.GetOriFlg( LocalHitVec, LocalHitPoint1 );

		// 判定を折る.
		Vector2[] Polygon = new Vector2[3];
		Vector2	  Point = new Vector2();
		if( origamiCollider == null )	return false;
		for( int i = 0; i < origamiCollider.RayPoint.Length; i++ ){
			if( !origamiCollider.RayPoint[i].Enable )	continue;
			if( GetSide( LocalHitVec, origamiCollider.RayPoint[i].Position - LocalHitPoint1 ) == OriFlg ){
				float Len = StaticMath.RayDistancePoint( LocalHitPoint1, LocalHitPoint2, origamiCollider.RayPoint[i].Position, ref CrossPoint );
				if( OriFlg ){
					origamiCollider.RayPoint[i].Position -= LocalNormal * Len * 2.0f;
				}
				else{
					origamiCollider.RayPoint[i].Position += LocalNormal * Len * 2.0f;
				}
				
				Point.Set( origamiCollider.RayPoint[i].Position.x, origamiCollider.RayPoint[i].Position.z );
				for( int j = 0; j < OptimizeIndex.Length; j+=3 ){
					Polygon[0].Set( OptimizeVertex[OptimizeIndex[j+0]].x, OptimizeVertex[OptimizeIndex[j+0]].z );
					Polygon[1].Set( OptimizeVertex[OptimizeIndex[j+1]].x, OptimizeVertex[OptimizeIndex[j+1]].z );
					Polygon[2].Set( OptimizeVertex[OptimizeIndex[j+2]].x, OptimizeVertex[OptimizeIndex[j+2]].z );
					if( StaticMath.CheckInPolygon2D( Polygon, 3, Point ) ){
						origamiCollider.RayPoint[i].Enable = false;
						break;
					}
				}
			}
		}
		
		// 折る前と折った後の頂点座標とインデックスを求める.
		int OriCnt = 0;
		for( int i = 0; i < OptimizeVertex.Length; i++ ){
			if( GetSide( LocalHitVec, OptimizeVertex[i] - LocalHitPoint1 ) == OriFlg ){
				OriCnt++;
			}
		}
		Vector3[] 	StartPos = new Vector3[OriCnt];
		Vector3[] 	EndPos = new Vector3[OriCnt];
		int[]		OrigamiIndex = new int[OriCnt];
		OriCnt = 0;
		for( int i = 0; i < OptimizeVertex.Length; i++ ){
			if( GetSide( LocalHitVec, OptimizeVertex[i] - LocalHitPoint1 ) == OriFlg ){
				float Len = StaticMath.RayDistancePoint( LocalHitPoint1, LocalHitPoint2, OptimizeVertex[i], ref CrossPoint );
				StartPos[OriCnt] = OptimizeVertex[i];
				if( OriFlg ){
					EndPos[OriCnt] = OptimizeVertex[i] - LocalNormal * Len * 2.0f;
				}
				else{
					EndPos[OriCnt] = OptimizeVertex[i] + LocalNormal * Len * 2.0f;
				}
				OrigamiIndex[OriCnt] = i;
				OriCnt ++;
			}
		}
		origamiCollider.SetStartPos( StartPos );
		origamiCollider.SetEndPos( EndPos );
		origamiCollider.SetOrigamiIndex( OrigamiIndex );
		
		return true;
	}
}
