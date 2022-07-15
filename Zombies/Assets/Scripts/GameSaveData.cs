using System; 

[Serializable]
public class GameSaveData{
  public Vec3Serializable playerPosition; 
  public ZombieData[] zombieDatas; 

  public int playerScore; 
  
}

[Serializable]
public class ZombieData{
  Vec3Serializable position; 
  bool isFemaleZombie; 

  public ZombieData(Vec3Serializable position, bool isFemale){
    this.position = position; 
    this.isFemaleZombie = isFemale; 
  }
}

[Serializable]
public class Vec3Serializable{
  public float x, y, z; 
  public Vec3Serializable(float x , float y, float z){
    this.x = x; 
    this.y = y; 
    this.z = z; 
  }
}