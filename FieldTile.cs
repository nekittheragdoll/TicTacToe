using Godot;
using System;

namespace TicTacToeResources
{
    public partial class FieldTile : TextureButton {

        public bool PieceExists { get; set; }
        public bool PlayerIsX { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
    
        public void ActivateTile()
        {
            this.TextureNormal = PlayerIsX ? ResourceLoader.Load("res://x_sprite.png") as Texture2D :
                                             ResourceLoader.Load("res://o_sprite.png") as Texture2D;           
        }
    }
    
}