using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AuditCheckList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuditCheckListController : ControllerBase
    {
         private readonly log4net.ILog _log4net;
        public AuditCheckListController()
        {
            _log4net = log4net.LogManager.GetLogger(typeof(AuditCheckListController));
        }
        
        [HttpGet]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public  ActionResult<List<string>> GetAuditCheckListQuestions(string AuditType)
        {
            List<string> Question = new List<string>();

              if (!AuditType.Equals("internal", StringComparison.InvariantCultureIgnoreCase)           //for invalid input type
                   && !AuditType.Equals("sox", StringComparison.InvariantCultureIgnoreCase))
            {
                _log4net.Info($"AuditChceklist Get Method invoked with Invalid AuditType");                              
                return BadRequest("Invalid Input");                                                            
            }

            _log4net.Info($"AuditChceklist Get Method invoked with {AuditType} AuditType");


            if (AuditType.Equals("internal", StringComparison.InvariantCultureIgnoreCase))            //internal audit type
            {
                Question.Add("Have all Change requests followed SDLC before PROD move? ");
                Question.Add("Have all Change requests been approved by the application owner?");                            
                Question.Add("Are all artifacts like CR document, Unit test cases available?");
                Question.Add("Is the SIT and UAT sign-off available?");
                Question.Add("Is data deletion from the system done with application owner approval?");
            }
            else if(AuditType.Equals("sox", StringComparison.InvariantCultureIgnoreCase))              //SOX Audit TYpe
            {
                Question.Add("Have all Change requests followed SDLC before PROD move?");
                Question.Add("Have all Change requests been approved by the application owner?");
                Question.Add("For a major change, was there a database backup taken before and after PROD move?");             
                Question.Add("Has the application owner approval obtained while adding a user to the system?");
                Question.Add("Is data deletion from the system done with application owner approval?");
            }
             return Ok(Question);                                                                          //return 200 status code                                                                

        }
    }
}