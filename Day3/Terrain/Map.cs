using System;
using System.Collections.Generic;
using System.Text;

namespace Day3.Terrain
{
    public class Map
    {
        private List<MapRow> _mapRows = new List<MapRow>();
        public Map(string mapData)
        {
            string[] rows = mapData.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            foreach (string aRow in rows)
                this._mapRows.Add(new MapRow(aRow));
        }

        public GridCellType this[int column, int row]
        {
            get
            {
                if (this._mapRows.Count == 0 || row < 0 || row >= this._mapRows.Count)
                    return GridCellType.nothing; // indicates we have go out side the map and there is nothing there

                MapRow aRow = this._mapRows[row];

                return aRow[column];
            }
        }
    }

    public class MapRow
    {
        private string _RawData;
        private int _RowLength;

        public MapRow(string rowData)
        {
            this._RawData = rowData;
            this._RowLength = this._RawData.Length;
        }

        public GridCellType this[int column]
        {
            get
            {
                //if (this._RowLength == 0 || column < 0 || column > this._RowLength)
                if (this._RowLength == 0 || column < 0)
                    return GridCellType.nothing;// indicates we have go out side the map and there is nothing there

                //switch(this._RawData[column])
                switch(this._RawData[this.convertColumnNumberToMap(column)])
                {
                    case '.':
                        return GridCellType.OpenSquare;

                    case '#':
                        return GridCellType.Tree;

                    default:
                        return GridCellType.unknown;
                }
            }
            
        }

        /// <summary>
        /// the map repeates it self many times over to the right.
        /// Instead of acutaly reating the map, work out the position the user is looking at on the map
        /// in relation to the map we have
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private int convertColumnNumberToMap(int column)
        {
            if (column < this._RowLength)
                return column;

            return column % this._RowLength;
        }
    }

    public enum GridCellType 
    { 
        nothing, // indicates we have gone outside the map and there is nothing there
        OpenSquare, // an open square '.' on the map
        Tree, // a tree '#' on the map
        unknown // we dont' know what this is
    }

}
