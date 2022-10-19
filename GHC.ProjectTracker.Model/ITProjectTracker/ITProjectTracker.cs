using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ProjectTracker.Model.ITProjectTracker
{
    public class ITProjectTracker
    {
        public int Id { get; set; }
        public string ProjectReference { get; set; }
        public string ProjectName { get; set; }
        public string Path { get; set; }
        public int? NewOrExisting { get; set; }
        public string ProjectDescription { get; set; }
        public int? Category { get; set; }
        public int? ProjectType { get; set; }
        public int? SystemType { get; set; }
        public int? BusinessProjectManager { get; set; }
        public int? ITProjectManager { get; set; }
        public int? Status { get; set; }
        public int? Priority { get; set; }
        public int? BusinessContactId { get; set; }
        public int? TechnicalLeadId { get; set; }
        public decimal ApprovedBudgetExternalCost { get; set; }
        public int? ExternalCostCurrency { get; set; }
        public int[] Review { get; set; }
        public int[] OpCoId { get; set; }
        public int[] DocumentaionId { get; set; }
        public int[] Developers { get; set; }
        public decimal? ForecastExternalCost { get; set; }
        public DateTime? BaselineGoLiveDate { get; set; }
        public DateTime? TargetGoLiveDate { get; set; }
        public string Comments { get; set; }
        public string IssuesAndResolutions { get; set; }
        public DateTime? DateModified { get; set; }
        public int? ModifiedBy { get; set; }
        public string ChangesSinceLastIssue { get; set; }
        public bool ClosedForReporting { get; set; }
        public bool KeyProject { get; set; }
        public DateTime? GW1ProjectBriefApprovalDate { get; set; }
        public DateTime? GW2ProjectBriefApprovalDate { get; set; }
        public DateTime? GW4GoLiveApprovalDate { get; set; }
        public DateTime? GW5ProjectClosureApprovalDate { get; set; }
        public int? ProjectRequestedBy { get; set; }
        public int? StrategyLevel { get; set; }
        public int? StrategyGroup { get; set; }
        public int? SystemsStrategy { get; set; }
        public DateTime? StartYear { get; set; }
        public DateTime? GW0ProjectKickOffDate { get; set; }
        public DateTime? GW3DesignDate { get; set; }
        public DateTime? ProjectAddedDate { get; set; }
        public string ProgCommComments { get; set; }
        public int? ProjectRequest { get; set; }
        public int? RiskRegisterFlag { get; set; }
        public int? ChangeRegisterFlag { get; set; }
        public int? ProjectPlanFlag { get; set; }
        public int? TestPlanFlag { get; set; }

        //Mappings with System Lookup table
        [ForeignKey("Category")]
        public virtual SystemLookup Categories { get; set; }

        [ForeignKey("ProjectType")]
        public virtual SystemLookup ProjectTypes { get; set; }

        [ForeignKey("SystemType")]
        public virtual SystemLookup SystemTypes { get; set; }

        [ForeignKey("Status")]
        public virtual SystemLookup Statuses { get; set; }

        [ForeignKey("Priority")]
        public virtual SystemLookup Priorities { get; set; }

        [ForeignKey("ExternalCostCurrency")]
        public virtual SystemLookup ExternalCostCurrencies { get; set; }

        [ForeignKey("StrategyLevel")]
        public virtual SystemLookup StrategyLevels { get; set; }

        [ForeignKey("StrategyGroup")]
        public virtual SystemLookup StrategyGroups { get; set; }

        [ForeignKey("SystemsStrategy")]
        public virtual SystemLookup SystemStrategies { get; set; }

        [ForeignKey("ProjectRequest")]
        public virtual SystemLookup ProjectRequests { get; set; }

        [ForeignKey("RiskRegisterFlag")]
        public virtual SystemLookup RiskRegisterFlags { get; set; }

        [ForeignKey("ChangeRegisterFlag")]
        public virtual SystemLookup ChangeRegisterFlags { get; set; }

        [ForeignKey("TestPlanFlag")]
        public virtual SystemLookup TestPlanFlags { get; set; }

        //Mappings with User table
        [ForeignKey("BusinessProjectManager")]
        public virtual User BusinessProjectManagers { get; set; }

        [ForeignKey("ITProjectManager")]
        public virtual User ITProjectManagers { get; set; }

        [ForeignKey("ProjectRequestedBy")]
        public virtual User ProjectRequestedByUsers { get; set; }

 


        //Not Mapped Attributes
        [NotMapped]
        public string SystemTypeName { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }

        [NotMapped]
        public string ProjectTypeName { get; set; }

        [NotMapped]
        public string ProjectRequestName { get; set; }

        [NotMapped]
        public string PriorityName { get; set; }
        
        [NotMapped]
        public bool IsUpdated { get; set; }


        [NotMapped]
        public string UpdatedBy { get; set; }

        [NotMapped]
        public string BusinessProjectManagerName { get; set; }

        [NotMapped]
        public string ProjectRequestedByUserName { get; set; }

        [NotMapped]
        public string ITProjectManagerName { get; set; }

        [NotMapped]
        public string ExternalCostCurrencyName { get; set; }

        [NotMapped]
        public string StrategyLevelName { get; set; }

        [NotMapped]
        public string StrategyGroupName { get; set; }

        [NotMapped]
        public string SystemStrategyName { get; set; }

        [NotMapped]
        public string StatusName { get; set; }

        //Flags
        [NotMapped]
        public string RiskRegisterFlagName { get; set; }

        [NotMapped]
        public string ChangeRegisterFlagName { get; set; }

        [NotMapped]
        public string ProjectPlanFlagName { get; set; }

        [NotMapped]
        public string TestPlanFlagName { get; set; }
    }
}
