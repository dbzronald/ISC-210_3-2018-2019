using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities
{
    public class TaskAction
    {
        public enum TaskActionType
        {
            ShowMessage,
            InventoryAdd,
            LoadScene
        }

        public TaskActionType Type { get; set; }
        public string uniqueObjectNameFrom { get; set; }
        public string uniqueObjectNameTo { get; set; }
        public float Quantity { get; set; }
    }
}
