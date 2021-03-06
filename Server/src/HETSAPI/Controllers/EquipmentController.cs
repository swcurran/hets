using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using HETSAPI.Models;
using HETSAPI.ViewModels;
using HETSAPI.Services;
using HETSAPI.Authorization;
using HETSAPI.Services.Impl;

namespace HETSAPI.Controllers
{
    /// <summary>
    /// Equipment Controller
    /// </summary>
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class EquipmentController : Controller
    {
        private readonly IEquipmentService _service;

        /// <summary>
        /// Equipment Controller Constructor
        /// </summary>
        public EquipmentController(IEquipmentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create bulk equipment records
        /// </summary>
        /// <param name="items"></param>
        /// <response code="200">Equipment created</response>
        [HttpPost]
        [Route("/api/equipment/bulk")]
        [SwaggerOperation("EquipmentBulkPost")]
        [RequiresPermission(Permission.Admin)]
        public virtual IActionResult EquipmentBulkPost([FromBody]Equipment[] items)
        {
            return _service.EquipmentBulkPostAsync(items);
        }                

        /// <summary>
        /// Get equipment by id
        /// </summary>
        /// <param name="id">id of Equipment to fetch</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/equipment/{id}")]
        [SwaggerOperation("EquipmentIdGet")]
        [SwaggerResponse(200, type: typeof(Equipment))]
        [RequiresPermission(Permission.Login)]
        public virtual IActionResult EquipmentIdGet([FromRoute]int id)
        {
            return _service.EquipmentIdGetAsync(id);
        }
        
        /// <summary>
        /// Update equipment
        /// </summary>
        /// <param name="id">id of Equipment to update</param>
        /// <param name="item"></param>
        /// <response code="200">OK</response>
        [HttpPut]
        [Route("/api/equipment/{id}")]
        [SwaggerOperation("EquipmentIdPut")]
        [SwaggerResponse(200, type: typeof(Equipment))]
        [RequiresPermission(Permission.Login)]
        public virtual IActionResult EquipmentIdPut([FromRoute]int id, [FromBody]Equipment item)
        {
            return _service.EquipmentIdPutAsync(id, item);
        }

        /// <summary>
        /// Update equipment status
        /// </summary>
        /// <param name="id">id of Equipment to update</param>
        /// <param name="item"></param>
        /// <response code="200">OK</response>
        [HttpPut]
        [Route("/api/equipment/{id}/status")]
        [SwaggerOperation("EquipmentIdStatusPut")]
        [SwaggerResponse(200, type: typeof(Equipment))]
        [RequiresPermission(Permission.Login)]
        public virtual IActionResult EquipmentIdStatusPut([FromRoute]int id, [FromBody]EquipmentStatus item)
        {
            return _service.EquipmentIdStatusPutAsync(id, item);
        }

        /// <summary>
        /// Create equipment
        /// </summary>
        /// <param name="item"></param>
        /// <response code="200">Equipment created</response>
        [HttpPost]
        [Route("/api/equipment")]
        [SwaggerOperation("EquipmentPost")]
        [SwaggerResponse(200, type: typeof(Equipment))]
        [RequiresPermission(Permission.Login)]
        public virtual IActionResult EquipmentPost([FromBody]Equipment item)
        {
            return _service.EquipmentPostAsync(item);
        }

        /// <summary>
        /// Searches Equipment
        /// </summary>
        /// <remarks>Used for the equipment search page.</remarks>
        /// <param name="localareas">Local Areas (comma seperated list of id numbers)</param>
        /// <param name="types">Equipment Types (comma seperated list of id numbers)</param>
        /// <param name="equipmentAttachment">Searches equipmentAttachment type</param>
        /// <param name="owner"></param>
        /// <param name="status">Status</param>
        /// <param name="hired">Hired</param>
        /// <param name="notverifiedsincedate">Not Verified Since Date</param>
        /// <param name="equipmentId">Equipment Code</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/equipment/search")]
        [SwaggerOperation("EquipmentSearchGet")]
        [SwaggerResponse(200, type: typeof(List<EquipmentViewModel>))]
        [RequiresPermission(Permission.Login)]
        public virtual IActionResult EquipmentSearchGet([FromQuery]string localareas, [FromQuery]string types, [FromQuery]string equipmentAttachment, [FromQuery]int? owner, [FromQuery]string status, [FromQuery]bool? hired, [FromQuery]DateTime? notverifiedsincedate, [FromQuery]string equipmentId = null)
        {
            return _service.EquipmentSearchGetAsync(localareas, types, equipmentAttachment, owner, status, hired, notverifiedsincedate, equipmentId);
        }        

        #region Clone Project Agreements

        /// <summary>
        /// Get renatal agreements associated with an equipment id
        /// </summary>
        /// <remarks>Gets as Equipment&#39;s Rental Agreements</remarks>
        /// <param name="id">id of Equipment to fetch agreements for</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/equipment/{id}/rentalAgreements")]
        [SwaggerOperation("EquipmentIdRentalAgreementsGet")]
        [SwaggerResponse(200, type: typeof(List<RentalAgreement>))]
        [RequiresPermission(Permission.Login)]
        public virtual IActionResult EquipmentIdRentalAgreementsGet([FromRoute]int id)
        {
            return _service.EquipmentIdGetAgreementsAsync(id);
        }

        /// <summary>
        /// Update a rental agreement by cloning a previous equipment rental agreement
        /// </summary>
        /// <param name="id">Project id</param>
        /// <param name="item"></param>
        /// <response code="200">Rental Agreement cloned</response>
        [HttpPost]
        [Route("/api/equipment/{id}/rentalAgreementClone")]
        [SwaggerOperation("EquipmentRentalAgreementClonePost")]
        [SwaggerResponse(200, type: typeof(RentalAgreement))]
        [RequiresPermission(Permission.Login)]
        public virtual IActionResult EquipmentRentalAgreementClonePost([FromRoute]int id, [FromBody]EquipmentRentalAgreementClone item)
        {
            return _service.EquipmentRentalAgreementClonePostAsync(id, item);
        }

        #endregion

        #region Duplicate Equipment Records

        /// <summary>
        /// Get all duplicate equipment records
        /// </summary>
        /// <param name="id">id of Equipment to fetch EquipmentAttachments for</param>
        /// <param name="serialNumber"></param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/equipment/{id}/duplicates/{serialNumber}")]
        [SwaggerOperation("EquipmentIdEquipmentduplicatesGet")]
        [SwaggerResponse(200, type: typeof(List<DuplicateEquipmentViewModel>))]
        [RequiresPermission(Permission.Login)]
        public virtual IActionResult EquipmentIdEquipmentduplicatesGet([FromRoute]int id, [FromRoute]string serialNumber)
        {
            return _service.EquipmentIdEquipmentduplicatesGetAsync(id, serialNumber);
        }

        #endregion

        #region Equipment Attachment Records

        /// <summary>
        /// Get all equipment attachments for an equipment record
        /// </summary>
        /// <param name="id">id of Equipment to fetch EquipmentAttachments for</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/equipment/{id}/equipmentattachments")]
        [SwaggerOperation("EquipmentIdEquipmentattachmentsGet")]
        [SwaggerResponse(200, type: typeof(List<EquipmentAttachment>))]
        [RequiresPermission(Permission.Login)]
        public virtual IActionResult EquipmentIdEquipmentattachmentsGet([FromRoute]int id)
        {
            return _service.EquipmentIdEquipmentattachmentsGetAsync(id);
        }

        #endregion

        #region Attachments

        /// <summary>
        /// Get all attachments associated with an equipment record
        /// </summary>
        /// <remarks>Returns attachments for a particular Equipment</remarks>
        /// <param name="id">id of Equipment to fetch attachments for</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/equipment/{id}/attachments")]
        [SwaggerOperation("EquipmentIdAttachmentsGet")]
        [SwaggerResponse(200, type: typeof(List<AttachmentViewModel>))]
        [RequiresPermission(Permission.Login)]
        public virtual IActionResult EquipmentIdAttachmentsGet([FromRoute]int id)
        {
            return _service.EquipmentIdAttachmentsGetAsync(id);
        }

        #endregion

        #region Equipment History Records

        /// <summary>
        /// Get equipment history
        /// </summary>
        /// <remarks>Returns History for a particular Equipment</remarks>
        /// <param name="id">id of Equipment to fetch History for</param>
        /// <param name="offset">offset for records that are returned</param>
        /// <param name="limit">limits the number of records returned.</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/equipment/{id}/history")]
        [SwaggerOperation("EquipmentIdHistoryGet")]
        [SwaggerResponse(200, type: typeof(List<HistoryViewModel>))]
        [RequiresPermission(Permission.Login)]
        public virtual IActionResult EquipmentIdHistoryGet([FromRoute]int id, [FromQuery]int? offset, [FromQuery]int? limit)
        {
            return _service.EquipmentIdHistoryGetAsync(id, offset, limit);
        }

        /// <summary>
        /// Create equipment history
        /// </summary>
        /// <remarks>Add a History record to the Equipment</remarks>
        /// <param name="id">id of Equipment to add History for</param>
        /// <param name="item"></param>
        /// <response code="200">OK</response>
        /// <response code="201">History created</response>
        [HttpPost]
        [Route("/api/equipment/{id}/history")]
        [SwaggerOperation("EquipmentIdHistoryPost")]
        [RequiresPermission(Permission.Login)]
        public virtual IActionResult EquipmentIdHistoryPost([FromRoute]int id, [FromBody]History item)
        {
            return _service.EquipmentIdHistoryPostAsync(id, item);
        }

        #endregion

        #region Equipment Note Records

        /// <summary>
        /// Get note records associated with equipment
        /// </summary>
        /// <param name="id">id of Equipment to fetch Notes for</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/equipment/{id}/notes")]
        [SwaggerOperation("EquipmentsIdNotesGet")]
        [SwaggerResponse(200, type: typeof(List<Note>))]
        [RequiresPermission(Permission.Login)]
        public virtual IActionResult EquipmentIdNotesGet([FromRoute]int id)
        {
            return _service.EquipmentIdNotesGetAsync(id);
        }

        /// <summary>
        /// Update or create a note associated with a equipment
        /// </summary>
        /// <remarks>Update a Equipment&#39;s Notes</remarks>
        /// <param name="id">id of Equipment to update Notes for</param>
        /// <param name="item">Equipment Note</param>
        /// <response code="200">OK</response>
        [HttpPost]
        [Route("/api/equipment/{id}/note")]
        [SwaggerOperation("EquipmentIdNotePost")]
        [SwaggerResponse(200, type: typeof(Note))]
        [RequiresPermission(Permission.Login)]
        public virtual IActionResult EquipmentIdNotePost([FromRoute]int id, [FromBody]Note item)
        {
            return _service.EquipmentIdNotesPostAsync(id, item);
        }

        /// <summary>
        /// pdate or create an array of notes associated with a equipment
        /// </summary>
        /// <remarks>Adds Note Records</remarks>
        /// <param name="id">id of Equipment to add notes for</param>
        /// <param name="items">Array of Equipment Notes</param>
        /// <response code="200">OK</response>
        [HttpPost]
        [Route("/api/equipment/{id}/notes")]
        [SwaggerOperation("EquipmentIdNotesBulkPostAsync")]
        [SwaggerResponse(200, type: typeof(TimeRecord))]
        [RequiresPermission(Permission.Login)]
        public virtual IActionResult EquipmentIdNotesBulkPostAsync([FromRoute]int id, [FromBody]Note[] items)
        {
            return _service.EquipmentIdNotesBulkPostAsync(id, items);
        }

        #endregion

        #region Seniority List Pdf

        /// <summary>
        /// Get a pdf version of the seniority list
        /// </summary>
        /// <remarks>Returns a PDF version of the seniority list</remarks>
        /// <param name="localareas">Local Areas (comma seperated list of id numbers)</param>
        /// <param name="types">Equipment Types (comma seperated list of id numbers)</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/equipment/seniorityListPdf")]
        [SwaggerOperation("EquipmentSeniorityListPdfGet")]
        [RequiresPermission(Permission.Login)]
        public virtual IActionResult EquipmentSeniorityListPdfGet([FromQuery]string localareas, [FromQuery]string types)
        {
            return _service.EquipmentSeniorityListPdfGetAsync(localareas, types);
        }

        #endregion
    }
}
