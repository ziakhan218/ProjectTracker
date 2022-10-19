using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GHC.ProjectTracker.Data;
using GHC.ProjectTracker.Model;
using GHC.ProjectTracker.Model.ITProjectTracker;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using GHC.ITPTracker.Model.ITProjectTracker;

namespace GHC.ProjectTracker.Business
{

    public class ProjectTrackerManager : IProjectTrackerManager
    {
        private readonly IProjectTrackerRepository projectTrackerRepository;
        public ProjectTrackerManager(IProjectTrackerRepository projectTrackerRepository)
        {
            this.projectTrackerRepository = projectTrackerRepository;
        }

        public IEnumerable<ITProjectTracker> GetAllProjects()
        {
            return projectTrackerRepository.GetAllProjects();
        }

        public IEnumerable<SystemLookup> GetDropdownOpt(List<string> categories)
        {
            return projectTrackerRepository.GetDropdownOpt(categories);
        }
        public ITProjectTracker GetProjectById(string projectId)
        {
            return projectTrackerRepository.GetProjectById(projectId);
        }
        public bool SaveProject(ITProjectTracker project, int[] reviews, int[] opCo, int[] reqDoc)
        {
            return projectTrackerRepository.SaveProject(project, reviews, opCo, reqDoc);
        }

        public bool DeleteProjects(int[] projects)
        {
            return projectTrackerRepository.DeleteProjects(projects);
        }

        public int[] GetAllOpCo(int projectId)
        {
            return projectTrackerRepository.GetAllOpCo(projectId);
        }

        public int[] GetAllReview(int projectId)
        {
            return projectTrackerRepository.GetAllReview(projectId);
        }

        public int[] RequiredDocumentation(int projectId)
        {
            return projectTrackerRepository.RequiredDocumentation(projectId);
        }

        public int[] DeveloperIds(int projectId)
        {
            return projectTrackerRepository.DeveloperIds(projectId);
        }

        public bool ProjectName(string name)
        {
            return projectTrackerRepository.ProjectName(name);
        }
        public List<WorkItemandChildLinks> GetAllBacklogItems(string projectName)
        {
            List<WorkItemandChildLinks> trees = new List<WorkItemandChildLinks>();
            List<WorkItemandChildLinks> list = new List<WorkItemandChildLinks>();
            var username = "dev.team@grosvenor.com";
            var password = "Grosvenor02";
            string _credentials = Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", username, password)));


            var wiql = new
            {
                query = "Select [State], [Title] ,[Description]" +
                        "From WorkItems " +
                        "Where  [System.TeamProject] = '" + projectName + "' " +
                        "Order By [State] Asc, [Changed Date] Desc"
            };

            var idslist = new List<IDS>();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://grosvenorbi.visualstudio.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _credentials);

                //serialize the wiql object into a json string   
                var postValue = new StringContent(JsonConvert.SerializeObject(wiql), Encoding.UTF8, "application/json"); //mediaType needs to be application/json for a post call

                var method = new HttpMethod("POST");
                var httpRequestMessage = new HttpRequestMessage(method, "https://grosvenorbi.visualstudio.com/_apis/wit/wiql?api-version=1.0") { Content = postValue };
                var httpResponseMessage = client.SendAsync(httpRequestMessage).Result;
                string a = httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var workItemQueryResult = httpResponseMessage.Content.ReadAsStringAsync().Result; ;
                    WorkItemQueryResult Details = JsonConvert.DeserializeObject<WorkItemQueryResult>(workItemQueryResult);


                    var builder = new System.Text.StringBuilder();

                    foreach (var item in Details.workItems)
                    {
                        builder.Append(item.id.ToString()).Append(",");
                    }
                    
                    string ids = builder.ToString().TrimEnd(new char[] { ',' });
                    
                    HttpResponseMessage getWorkItemsHttpResponse = client.GetAsync("_apis/wit/workitems?ids=" + ids + "&$expand=all&api-version=1.0").Result;

                    if (getWorkItemsHttpResponse.IsSuccessStatusCode)
                    {
                        var result = getWorkItemsHttpResponse.Content.ReadAsStringAsync().Result;

                        WorkItemsValues woiDetails = JsonConvert.DeserializeObject<WorkItemsValues>(result);
                        foreach (var item in woiDetails.value)
                        {
                            WorkItemandChildLinks parent = new WorkItemandChildLinks();
                            parent.id = Convert.ToInt32(item.id);


                            if (item.fields.Keys.Contains("System.AssignedTo"))
                            {
                                parent.Assignedto = item.fields.FirstOrDefault(x => x.Key == "System.AssignedTo").Value;
                            }
                            if (item.fields.Keys.Contains("System.Title"))
                            {
                                parent.Task = item.fields.FirstOrDefault(x => x.Key == "System.Title").Value;

                            }
                            if (item.fields.Keys.Contains("System.State"))
                            {
                                parent.State = item.fields.FirstOrDefault(x => x.Key == "System.State").Value;

                            }

                            if (item.fields.FirstOrDefault(x => x.Key == "Microsoft.VSTS.Common.ValueArea").Value == "Business")
                            {
                                
                                if (item.relations != null)
                                {
                                    foreach (var c in item.relations)
                                    {
                                        IDS idss = new IDS();
                                        idss.parentid = Convert.ToInt32(item.id);
                                        int id;
                                        Child chd = new Child();
                                        bool asd = c.url.Split('/').Last().Contains("-");
                                        if ((!asd))
                                        {
                                            id = Convert.ToInt32(c.url.Split('/').Last());
                                        }
                                        else
                                        {
                                            continue;
                                            //id = 0;
                                        }

                                        idss.childid = id;


                                        idslist.Add(idss);
                                    }
                                }
                                else
                                {
                                    IDS idss = new IDS();
                                    idss.parentid = Convert.ToInt32(item.id);
                                    idslist.Add(idss);
                                }



                            }
                            list.Add(parent);


                        }

                        foreach (var item in list.ToList())
                        {
                            List<Child> childsss = new List<Child>();
                            Child ch = new Child();
                            var parentId = idslist.Find(x => x.parentid == item.id)?.parentid;
                            var childIds = idslist.Where(s => s.parentid == parentId).ToList();                            
                            foreach (var c in childIds)
                            {
                                var q = (from x in list
                                         where (x.id == c.childid)
                                         select new Child
                                         {
                                             Assignedto = x.Assignedto,
                                             Task = x.Task,
                                             State = x.State,
                                             TimeRemaining = x.TimeRemaining

                                         }).FirstOrDefault();

                                childsss.Add(q);
                                var re = list.SingleOrDefault(x => x.id == c.childid);
                                if (re != null)
                                    list.Remove(re);
                                
                            }

                            item.childs = childsss;

                        }
                        
                    }
                }

            }
            return list;

        }



        public bool SaveWorkItems(string ProjectName)
        {

            
            List<WorkItemandChildLinks> list = new List<WorkItemandChildLinks>();
            List<WorkItem> workItemList = new List<WorkItem>();
            List<WorkItemLink> workItemKinkList = new List<WorkItemLink>();
            var username = "dev.team@grosvenor.com";
            var password = "Grosvenor02";
            string _credentials = Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", username, password)));

            
            var wiql = new
            {
                query = "Select [State], [Title] ,[Description]" +
                        "From WorkItems " +
                        "Where  [System.TeamProject] = '" + ProjectName + "' " +
                        "Order By [State] Asc, [Changed Date] Desc"
            };

            var idslist = new List<IDS>();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://grosvenorbi.visualstudio.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _credentials);

                var postValue = new StringContent(JsonConvert.SerializeObject(wiql), Encoding.UTF8, "application/json"); //mediaType needs to be application/json for a post call

                var method = new HttpMethod("POST");
                var httpRequestMessage = new HttpRequestMessage(method, "https://grosvenorbi.visualstudio.com/_apis/wit/wiql?api-version=1.0") { Content = postValue };
                var httpResponseMessage = client.SendAsync(httpRequestMessage).Result;
                string a = httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var workItemQueryResult = httpResponseMessage.Content.ReadAsStringAsync().Result; ;
                    WorkItemQueryResult Details = JsonConvert.DeserializeObject<WorkItemQueryResult>(workItemQueryResult);

                    var builder = new System.Text.StringBuilder();

                    foreach (var item in Details.workItems)
                    {
                        builder.Append(item.id.ToString()).Append(",");
                    }                    
                    string ids = builder.ToString().TrimEnd(new char[] { ',' });                    
                    HttpResponseMessage getWorkItemsHttpResponse = client.GetAsync("_apis/wit/workitems?ids=" + ids + "&$expand=all&api-version=1.0").Result;

                    if (getWorkItemsHttpResponse.IsSuccessStatusCode)
                    {
                        var result = getWorkItemsHttpResponse.Content.ReadAsStringAsync().Result;

                        WorkItemsValues woiDetails = JsonConvert.DeserializeObject<WorkItemsValues>(result);
                        foreach (var item in woiDetails.value)
                        {
                            WorkItemandChildLinks parent = new WorkItemandChildLinks();
                            WorkItem work = new WorkItem();
                            parent.id = Convert.ToInt32(item.id);

                            work.ProjectName = "DataPortal";
                            if (item.fields.Keys.Contains("System.AssignedTo"))
                            {
                                parent.Assignedto = item.fields.FirstOrDefault(x => x.Key == "System.AssignedTo").Value;
                                work.AssignedTo = parent.Assignedto;
                            }
                            if (item.fields.Keys.Contains("System.Title"))
                            {
                                parent.Task = item.fields.FirstOrDefault(x => x.Key == "System.Title").Value;
                                work.Task = parent.Task;

                            }
                            if (item.fields.Keys.Contains("System.State"))
                            {
                                parent.State = item.fields.FirstOrDefault(x => x.Key == "System.State").Value;


                            }

                            if (item.fields.FirstOrDefault(x => x.Key == "Microsoft.VSTS.Common.ValueArea").Value == "Business")
                            {
                                
                                if (item.relations != null)
                                {
                                    foreach (var c in item.relations)
                                    {
                                        IDS idss = new IDS();
                                        idss.parentid = Convert.ToInt32(item.id);
                                        int id;
                                        Child chd = new Child();

                                        bool asd = c.url.Split('/').Last().Contains("-");
                                        if ((!asd))
                                        {
                                            id = Convert.ToInt32(c.url.Split('/').Last());
                                        }
                                        else
                                        {
                                            continue;
                                            //id = 0;
                                        }

                                        idss.childid = id;


                                        idslist.Add(idss);
                                    }
                                }
                                else
                                {
                                    IDS idss = new IDS();
                                    idss.parentid = Convert.ToInt32(item.id);
                                    idslist.Add(idss);
                                }



                            }
                            
                            list.Add(parent);


                        }

                        foreach (var item in list)
                        {
                            var parentId = idslist.Find(x => x.childid == item.id)?.parentid;
                            if (parentId == null)
                            {
                                item.parentId = 0;
                            }
                            else
                            {
                                item.parentId = parentId.Value;
                            }
                        }
                        foreach (var item in list.ToList())
                        {
                            List<Child> childsss = new List<Child>();
                            Child ch = new Child();

                            var parentId = idslist.Find(x => x.parentid == item.id)?.parentid;
                            var childIds = idslist.Where(s => s.parentid == parentId).ToList();

                            foreach (var c in childIds)
                            {

                                var q = (from x in list
                                         where (x.id == c.childid && x.id != 0)
                                         select new WorkItemLink
                                         {
                                             AssignedTo = x.Assignedto,
                                             ParentId = x.parentId,
                                             Task = x.Task,
                                             State = x.State,
                                             RemainingTime = x.TimeRemaining
                                         }).FirstOrDefault();
                                if (q != null)
                                    workItemKinkList.Add(q);
                                var re = list.SingleOrDefault(x => x.id == c.childid);
                                if (re != null)
                                    list.Remove(re);
                            }
                            //item.childs = childsss;
                        }

                        var test = idslist.Select(x => x.parentid).Distinct().ToList();
                        foreach (var item in test)
                        {
                            var q = (from x in list
                                     where (x.id == item)
                                     select new WorkItem
                                     {
                                         AssignedTo = x.Assignedto,
                                         Task = x.Task,
                                         ProjectName = ProjectName,
                                         State = x.State,
                                         Parent = x.id
                                        
                                     }).FirstOrDefault();
                            if (q != null)
                            {
                                workItemList.Add(q);
                            }

                        }
                    }
                }

                projectTrackerRepository.SaveWorkItems(workItemList, workItemKinkList);

                return true;
            }
        }

        public bool SaveProjectResource(int[] Developers, ITProjectTracker Project)
        {
            return  projectTrackerRepository.SaveProjectResource(Developers, Project);
        }
    }
}
