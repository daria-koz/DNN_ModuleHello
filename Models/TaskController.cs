﻿using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Users;
using DotNetNuke.Web.Api;
using System;
using DotNetNuke.Security;

namespace Christoc.Modules.MyFirstModule.Models
{
    public class TaskController : DnnApiController
    {
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage HelloWorld()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Hello World!");
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetTasks(int moduleId)
        {
            try
            {
                var tasks = new TaskLogic().GetTasks(moduleId).ToJson();
                return Request.CreateResponse(HttpStatusCode.OK, tasks);
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }

        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public HttpResponseMessage AddTask(TaskToCreateDTO DTO)
        {
            try
            {
                var task = new Task()
                {
                    TaskName = DTO.TTC_TaskName,
                    TaskDescription = DTO.TTC_TaskDescription,
                    IsComplete = DTO.TTC_isComplete,
                    ModuleId = DTO.TTC_ModuleID,
                    UserId = DTO.TTC_UserId
                };
                TaskLogic tl = new TaskLogic();
                tl.AddTask(task);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }
    }
}