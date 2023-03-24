using Godot;
using System;

namespace TicTacToeResources
{
    public partial class FieldTile : TextureButton {

        public bool PieceExists { get; set; }
        public bool PlayerIsX { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
    
        public void ActivateTile(bool isX)
        {
            PieceExists = true;
            PlayerIsX = isX;
            this.TextureNormal = PlayerIsX ? ResourceLoader.Load("res://Images/x_sprite.png") as Texture2D :
                                             ResourceLoader.Load("res://Images/o_sprite.png") as Texture2D;           
        }
    }
    
}