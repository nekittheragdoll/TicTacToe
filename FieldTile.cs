using Godot;
using System;

namespace TicTacToeResources
{
    public partial class FieldTile : TextureButton {

        public bool PieceExists { get; set; }
        public bool PieceIsX { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Index { get; set; }
    }
    
}