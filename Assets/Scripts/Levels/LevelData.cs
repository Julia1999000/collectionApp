using System;
using UnityEngine;

namespace Levels {
    
    [CreateAssetMenu(fileName = "New LevelData", menuName = "Level Data", order = 51)]
    public class LevelData : ScriptableObject {

        [SerializeField] private string _id = Guid.NewGuid().ToString().ToUpper();
        [SerializeField] private int _number;
        [SerializeField] private int _countCellsHorizontally = MIN_COUNT_CELLS;
        [SerializeField] private int _countCellsVertically = MIN_COUNT_CELLS;
        
        public static string NAME = @"Level_{0}";
        public static int MIN_COUNT_CELLS = 1;
        public static int MAX_COUNT_CELLS = 100;
        
        public string LevelID {
            get => _id;
        }
        public int Number {
            get => _number;
            set => _number = value;
        }
        public int CountCellsHorizontally {
            get => _countCellsHorizontally;
            set => _countCellsHorizontally = value;
        }
        public int CountCellsVertically {
            get => _countCellsVertically;
            set => _countCellsVertically = value;
        }
    }
    
}
