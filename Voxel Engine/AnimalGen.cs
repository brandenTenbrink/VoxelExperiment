using UnityEngine;
using System.Collections;
using SimplexNoise;

public class AnimalGen  {

	int dirtHeight = TerrainGen.dirtHeight;
	float animalBaseHeight = 1;
	float animalNoise = 0.04f;
	float animalNoiseHeight = 3;

	float pigFrequency = 0.2f;
	int pigDensity = 2;

	GameObject pig;

	public void AnimalGeneration(Chunk chunk) 
	{
		for (int x = chunk.pos.x-3; x < chunk.pos.x + Chunk.chunkSize + 3; x++)
		{
			for (int z = chunk.pos.z-3; z < chunk.pos.z + Chunk.chunkSize+3; z++)
			{
				AnimalPositionGen(chunk, x, z);
			}
		}
	}

	public void AnimalPositionGen(Chunk chunk, int x, int z){
		int animalHeight = dirtHeight + Mathf.FloorToInt(animalBaseHeight);
		animalHeight += GetNoise(x, 100 , z, animalNoise, Mathf.FloorToInt(animalNoiseHeight));
		
		for (int y = chunk.pos.y-8; y < chunk.pos.y + Chunk.chunkSize; y++)
		{
			if (y == animalHeight && GetNoise(x, y, z, pigFrequency, 100) < pigDensity)
			{
				CreatePig(x,y+1,z);
			}
		}
	}

	public static int GetNoise(int x, int y, int z, float scale, int max) 
	{
		return Mathf.FloorToInt( (Noise.Generate(x*scale,y*scale, z*scale) + 1f) * (max/2f));
	}

	public void CreatePig(int x, int y, int z){
		pig = MonoBehaviour.Instantiate (Resources.Load ("Prefabs/Pig",typeof(GameObject)),new Vector3(x,y,z),Quaternion.identity) as GameObject;
		World.pigs.Add (pig);
	}
}
