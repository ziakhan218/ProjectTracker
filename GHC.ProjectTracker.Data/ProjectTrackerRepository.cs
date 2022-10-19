using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GHC.ProjectTracker.Data.EntityRepository;
using GHC.ProjectTracker.Data.Infrastructure;
using GHC.ProjectTracker.Model.ITProjectTracker;
using GHC.ProjectTracker.Model;
using System.Diagnostics;
using GHC.ITPTracker.Model.ITProjectTracker;
using GHC.DataPortal.Model.ITProjectTracker;

namespace GHC.ProjectTracker.Data
{
    public class ProjectTrackerRepository : RepositoryBase, IProjectTrackerRepository
    {
        private IITPTrackerUnitOfWork unitOfWork;

        public ProjectTrackerRepository(IITPTrackerUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<ITProjectTracker> GetAllProjects()
        {

            var q = (from x in ((IITPTrackerDbContext)DataContext).ITProjectTrackers
                     where x.Id != 0
                     join y in ((IITPTrackerDbContext)DataContext).SystemLookups on x.Category equals y.Id into categry
                     from cat in categry.DefaultIfEmpty()

                     join pt in ((IITPTrackerDbContext)DataContext).SystemLookups on x.SystemType equals pt.Id into SystemType
                     from ptn in SystemType.DefaultIfEmpty()

                     join prio in ((IITPTrackerDbContext)DataContext).SystemLookups on x.Priority equals prio.Id into Priority
                     from pri in Priority.DefaultIfEmpty()

                     join user in ((IITPTrackerDbContext)DataContext).Users on x.ModifiedBy equals user.Id into users
                     from u in users.DefaultIfEmpty()

                     select new
                     {
                         CategoryName = cat.Name,
                         SystemTypeName = ptn.Name,
                         PriorityName = pri.Name,
                         Project = x,
                         Users = u
                     }).ToList();



            var item = q.Select(x => new ITProjectTracker
            {
                ProjectReference = x.Project.ProjectReference,
                ProjectName = x.Project.ProjectName,
                CategoryName = x.CategoryName,
                SystemTypeName = x.SystemTypeName,
                PriorityName = x.PriorityName,
                UpdatedBy = x.Users?.FullName
            }).ToList();
            return item;
        }

        public IEnumerable<SystemLookup> GetDropdownOpt(List<string> categories)
        {


            var lookupList = (from sl in ((IITPTrackerDbContext)DataContext).SystemLookups
                              where sl.IsActive && categories.Contains(sl.Category)
                              select sl).ToList();
            return lookupList;
        }

        public ITProjectTracker GetProjectById(string projectName)
        {
            var q = (from x in ((IITPTrackerDbContext)DataContext).ITProjectTrackers
                     where x.ProjectName == projectName
                     join user in ((IITPTrackerDbContext)DataContext).Users on x.ModifiedBy equals user.Id into users
                     from u in users.DefaultIfEmpty()

                     select new
                     {
                         User = u,
                         x
                     }).FirstOrDefault();

            q.x.UpdatedBy = q.User?.FullName;
            return q.x;
        }
        /// <summary>
        /// Add/Update Record
        /// </summary>
        /// <param name="project"></param>
        /// <param name="reviews"></param>
        /// <param name="opCo"></param>
        /// <param name="reqDoc"></param>
        /// <returns></returns>
        public bool SaveProject(ITProjectTracker project, int[] reviews, int[] opCo, int[] reqDoc)
        {
            var projectExists = ((IITPTrackerDbContext)DataContext).ITProjectTrackers.Count(x => x.Id == project.Id) > 0;
            if (!projectExists)
            {
                project.NewOrExisting = ((IITPTrackerDbContext)DataContext).SystemLookups.Where(x => x.Category == "ProjectTracker.NewOrExisting" && x.Name == "New").Select(x => x.Id).Single();
                SetAdded(project);//after adding, it's Id column will automatically be updated

                //Add Data in ProjectTrackerReview Table
                foreach (var r in reviews)
                {
                    ProjectTrackerReview objReview = new ProjectTrackerReview
                    {
                        ProjectTrackerId = project.Id,
                        ReviewId = r
                    };
                    SetAdded(objReview);
                }
                //Add Data in ProjectTrackerOpco Table
                foreach (var r in opCo)
                {
                    ProjectTrackerOpco objOpco = new ProjectTrackerOpco
                    {
                        ProjectTrackerId = project.Id,
                        OpcoId = r
                    };
                    SetAdded(objOpco);
                }
                //Add Data in ProjectTrackerReqDoc Table
                foreach (var r in reqDoc)
                {
                    ProjectTrackerReqDoc objReqDoc = new ProjectTrackerReqDoc
                    {
                        ProjectTrackerId = project.Id,
                        ReqDocId = r
                    };
                    SetAdded(objReqDoc);
                }
            }
            else
            {
                project.NewOrExisting = ((IITPTrackerDbContext)DataContext).SystemLookups.Where(x => x.Category == "ProjectTracker.NewOrExisting" && x.Name == "Existing").Select(x => x.Id).Single();
                SetModified(project);

                var review = (from rev in ((IITPTrackerDbContext)DataContext).ProjectTrackerReview
                              where rev.ProjectTrackerId == project.Id
                              select rev).ToList();
                foreach (var item in review)
                {
                    SetDeleted(item);
                }


                var opco = (from op in ((IITPTrackerDbContext)DataContext).ProjectTrackerOpco
                            where op.ProjectTrackerId == project.Id
                            select op).ToList();
                foreach (var item in opco)
                {
                    SetDeleted(item);
                }


                var requDoc = (from r in ((IITPTrackerDbContext)DataContext).ProjectTrackerReqDoc
                               where r.ProjectTrackerId == project.Id
                               select r).ToList();
                foreach (var item in requDoc)
                {
                    SetDeleted(item);
                }

                foreach (var r in reviews)
                {
                    ProjectTrackerReview objReview = new ProjectTrackerReview
                    {
                        ProjectTrackerId = project.Id,
                        ReviewId = r
                    };
                    SetAdded(objReview);
                }

                foreach (var r in opCo)
                {
                    ProjectTrackerOpco objOpco = new ProjectTrackerOpco
                    {
                        ProjectTrackerId = project.Id,
                        OpcoId = r
                    };
                    SetAdded(objOpco);
                }

                foreach (var r in reqDoc)
                {
                    ProjectTrackerReqDoc objReqDoc = new ProjectTrackerReqDoc
                    {
                        ProjectTrackerId = project.Id,
                        ReqDocId = r
                    };
                    SetAdded(objReqDoc);
                }

            }
            unitOfWork.SaveChanges();
            return true;
        }

        /// <summary>
        /// Delete Projects
        /// </summary>
        /// <param name="projects"></param>
        /// <returns></returns>
        public bool DeleteProjects(int[] projects)
        {
            //Get all reviews for selected projects
            var reviews = (from rev in ((IITPTrackerDbContext)DataContext).ProjectTrackerReview
                           where projects.Contains(rev.ProjectTrackerId)
                           select rev).ToList();

            //Get all opco for selected projects
            var opco = (from op in ((IITPTrackerDbContext)DataContext).ProjectTrackerOpco
                        where projects.Contains(op.ProjectTrackerId)
                        select op).ToList();

            //Get all opco for selected projects
            var reqDoc = (from r in ((IITPTrackerDbContext)DataContext).ProjectTrackerReqDoc
                          where projects.Contains(r.ProjectTrackerId)
                          select r).ToList();
            if (reviews.Count > 0)
            {
                foreach (var r in reviews)
                {
                    SetDeleted(r);
                }
            }
            if (opco.Count > 0)
            {
                foreach (var op in opco)
                {
                    SetDeleted(op);
                }
            }
            if (reqDoc.Count > 0)
            {
                foreach (var rd in reqDoc)
                {
                    SetDeleted(rd);
                }
            }
            foreach (var i in projects)
            {
                ITProjectTracker projectTracker = new ITProjectTracker
                {
                    Id = i
                };
                SetDeleted(projectTracker);
            }
            return unitOfWork.SaveChanges();
        }

        public int[] GetAllOpCo(int projectId)
        {
            var query = (from x in ((IITPTrackerDbContext)DataContext).ProjectTrackerOpco
                         where x.ProjectTrackerId == projectId
                         select x.OpcoId).ToArray();
            return query;
        }

        public int[] GetAllReview(int projectId)
        {
            var query = (from x in ((IITPTrackerDbContext)DataContext).ProjectTrackerReview
                         where x.ProjectTrackerId == projectId
                         select x.ReviewId).ToArray();
            return query;
        }

        public int[] RequiredDocumentation(int projectId)
        {
            var query = (from x in ((IITPTrackerDbContext)DataContext).ProjectTrackerReqDoc
                         where x.ProjectTrackerId == projectId
                         select x.ReqDocId).ToArray();
            return query;
        }
        public int[] DeveloperIds(int projectId)
        {
            var query = (from x in ((IITPTrackerDbContext)DataContext).Developers
                         where x.ProjectId == projectId
                         select x.UserId).ToArray();
            return query;
        }

        public bool ProjectName(string name)
        {
            var projectExists = ((IITPTrackerDbContext)DataContext).ITProjectTrackers.Count(x => x.ProjectName == name) > 0;
            return projectExists;
        }

        public bool SaveWorkItems(IList<WorkItem> workItems, IList<WorkItemLink> workItemlinks)
        {
            try
            {
                var inputListTbl = new System.Data.DataTable("inputList");
                inputListTbl.Columns.Add("ParentId", typeof(int));
                inputListTbl.Columns.Add("AssignedTo", typeof(string));
                inputListTbl.Columns.Add("Task", typeof(string));
                inputListTbl.Columns.Add("State", typeof(string));                
                inputListTbl.Columns.Add("RemainingTime", typeof(string));

                foreach (var item in workItemlinks)
                {
                    System.Data.DataRow inputRow = inputListTbl.NewRow();
                    inputRow["ParentId"] = item.ParentId;
                    inputRow["AssignedTo"] = item.AssignedTo;
                    inputRow["Task"] = item.Task;                    
                    inputRow["State"] = item.State;
                    inputRow["RemainingTime"] = item.RemainingTime;
                    inputListTbl.Rows.Add(inputRow);
                }

                var inputParam = new System.Data.SqlClient.SqlParameter("@inputList", System.Data.SqlDbType.Structured);
                inputParam.TypeName = "ProjectTrackerInputList";
                inputParam.Value = inputListTbl;
                ExecuteSql("ProjectTracker_SaveInputs @inputList", inputParam);

                //for work items

                var workItemListTbl = new System.Data.DataTable("workIteminputList");
                workItemListTbl.Columns.Add("AssignedTo", typeof(string));
                workItemListTbl.Columns.Add("Task", typeof(string));
                workItemListTbl.Columns.Add("State", typeof(string));
                workItemListTbl.Columns.Add("ProjectName", typeof(string));
                workItemListTbl.Columns.Add("Parent", typeof(int));

                foreach (var item in workItems)
                {
                    System.Data.DataRow inputRow = workItemListTbl.NewRow();
                    inputRow["AssignedTo"] = item.AssignedTo;
                    inputRow["Task"] = item.Task;
                    inputRow["State"] = item.State;
                    inputRow["ProjectName"] = item.ProjectName;
                    inputRow["Parent"] = item.Parent;
                    workItemListTbl.Rows.Add(inputRow);
                }

                var workItemParam = new System.Data.SqlClient.SqlParameter("@workIteminputList", System.Data.SqlDbType.Structured);
                workItemParam.TypeName = "ProjectTrackerList";
                workItemParam.Value = workItemListTbl;
                ExecuteSql("WorkItem_Save @workIteminputList", workItemParam);

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool SaveProjectResource(int[] DeveloperId, ITProjectTracker Project)
        {

            var developers = (from rev in ((IITPTrackerDbContext)DataContext).Developers
                          where rev.ProjectId== Project.Id
                          select rev).ToList();
            if(DeveloperId !=null)
            if (developers.Count > 0 )
            {
                foreach (var item in developers)
                {
                    SetDeleted(item);
                }
                foreach (var item in DeveloperId)
                {
                    Developers d = new Developers
                    {
                        UserId = item,
                        ProjectId = Project.Id
                    };
                    SetAdded(d);
                }

            }
            else
            {
                foreach (var item in DeveloperId)
                {
                    Developers d = new Developers
                    {
                        UserId = item,
                        ProjectId = Project.Id
                    };
                    SetAdded(d);
                }
            }
          
            SetModified(Project);

            var res = unitOfWork.SaveChanges();
            return res;
        }
    }


}

