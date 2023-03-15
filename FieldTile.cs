using Godot;
using System;

namespace TicTacToeResources
{
    public partial class FieldTile : TextureButton {

        public bool PieceExists { get; set; }
        public bool PlayerIsX { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Index { get; set; }
    
        public void ActivateTile()
        {
            if (PlayerIsX)
            {
                this.TextureNormal = ResourceLoader.Load("res://x_sprite.png") as Texture2D;
            }
            else
            {
                this.TextureNormal = ResourceLoader.Load("res://o_sprite.png") as Texture2D;
            }
           
        }
    }
    
}