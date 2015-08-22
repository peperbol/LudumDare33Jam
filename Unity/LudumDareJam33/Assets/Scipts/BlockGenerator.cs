using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Coords = Block.Coords  ;

public class BlockGenerator : MonoBehaviour {
  public Block[] PossibleBlocks;

  public Transform player;

  protected List<Block> generatedBlocks = new List<Block>();

  public Block GetBlock(Coords coords) {
    return generatedBlocks.FindLast(i => i.Coordinates.x == coords.x && i.Coordinates.y == coords.y);
  }

  protected Block playerBlock;

  protected void genBlocksArroundPlayer() {
    Debug.Log(Block.Vector3toCoords(player.position).x +" "+ Block.Vector3toCoords(player.position).y);
    playerBlock = GenBlock(Block.Vector3toCoords(player.position));
    Block t = GenBlock(playerBlock.Coordinates.left);
    GenBlock(t.Coordinates.upper);
    GenBlock(t.Coordinates.lower);
    GenBlock(playerBlock.Coordinates.upper);
    GenBlock(playerBlock.Coordinates.lower);
    t = GenBlock(playerBlock.Coordinates.right);
    GenBlock(t.Coordinates.upper);
    GenBlock(t.Coordinates.lower);
  }

  protected Block GenBlock(Coords c) {
    if (GetBlock(c) == null) {
      int i = Mathf.FloorToInt(Random.Range(0f, PossibleBlocks.Length - Mathf.Epsilon));
      Block b = Instantiate(PossibleBlocks[i]);
      b.Init(c, this);
      generatedBlocks.Add(b);
      return b;
    }
    return GetBlock(c);
  }
  void Start() {
    genBlocksArroundPlayer();
  }
  void Update() {
    if (playerBlock != GetBlock(Block.Vector3toCoords(player.position))) {
      genBlocksArroundPlayer();
    }
  }
  
  
}
