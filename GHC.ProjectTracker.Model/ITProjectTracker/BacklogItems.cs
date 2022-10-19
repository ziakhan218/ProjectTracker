using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ProjectTracker.Model.ITProjectTracker
{
    public class WorkItemQueryResult
    {
        public string queryType { get; set; }
        public string queryResultType { get; set; }
        public DateTime asOf { get; set; }
        public Column[] columns { get; set; }
        public BacklogItems[] workItems { get; set; }
    }

    public class Column
    {
        public string referenceName { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }
    public class WorkItemsValues
    {
        public int count;
        public Value[] value;

    }

    public class Value
    {

        public string id;
        public IDictionary<string, string> fields;
        public string Task;
        public string AssignedTo;
        public string State;
        public string RemainingTime;
        public bool IsParent;
        public Relation[] relations;
    }

    public class Relation
    {
        public string rel;
        public string url;
    }


  

    public class Child
    {
        public int id;
        public int? parentId;
        public string Task;
        public string State;
        public string TimeRemaining;
        public string Assignedto;
    }

    public class WorkItemandChildLinks
    {
        public int id;
        public int? parentId;
        public string Task;
        public string State;
        public string TimeRemaining;
        public string Assignedto;
        public List<Child> childs;

    }

    public class IDS
    {
        public int parentid;
        public int childid;
    }
    public class BacklogItems
    {
        public int id { get; set; }
        public string url { get; set; }
    }
}
