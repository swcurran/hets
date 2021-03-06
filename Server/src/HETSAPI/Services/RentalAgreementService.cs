using Microsoft.AspNetCore.Mvc;
using HETSAPI.Models;

namespace HETSAPI.Services
{
    /// <summary>
    /// Rental Agreement Service
    /// </summary>
    public interface IRentalAgreementService
    {
        /// <summary>
        /// Create bulk rental agreement records
        /// </summary>
        /// <param name="items"></param>
        /// <response code="201">RentalAgreement created</response>
        IActionResult RentalagreementsBulkPostAsync(RentalAgreement[] items);

        /// <summary>
        /// Get rental agreement by id
        /// </summary>
        /// <param name="id">id of RentalAgreement to fetch</param>
        /// <response code="200">OK</response>
        /// <response code="404">RentalAgreement not found</response>
        IActionResult RentalagreementsIdGetAsync(int id);

        /// <summary>
        /// Get pdf of rental agreement
        /// </summary>
        /// <remarks>Returns a PDF version of the specified rental agreement</remarks>
        /// <param name="id">id of RentalAgreement to obtain the PDF for</param>
        /// <response code="200">OK</response>
        IActionResult RentalagreementsIdPdfGetAsync(int id);

        /// <summary>
        /// Update a rental agreement
        /// </summary>
        /// <param name="id">id of RentalAgreement to update</param>
        /// <param name="item"></param>
        /// <response code="200">OK</response>
        /// <response code="404">RentalAgreement not found</response>
        IActionResult RentalagreementsIdPutAsync(int id, RentalAgreement item);

        /// <summary>
        /// Create a rental agreement
        /// </summary>
        /// <param name="item"></param>
        /// <response code="201">RentalAgreement created</response>
        IActionResult RentalagreementsPostAsync(RentalAgreement item);

        /// <summary>
        /// Release (terminate) a rental agreement
        /// </summary>
        /// /// <param name="id">Id of Rental Agreement to release</param>
        /// <response code="201">Rental Agreement released</response>
        IActionResult RentalagreementsIdReleasePostAsync(int id);

        /// <summary>
        /// Get time records for a rental agreement
        /// </summary>
        /// <remarks>Gets a Rental Agreement&#39;s Time Records</remarks>
        /// <param name="id">id of Rental Agreement to fetch Time Records for</param>
        /// <response code="200">OK</response>
        IActionResult RentalAgreementsIdTimeRecordsGetAsync(int id);

        /// <summary>
        /// Add a time record to a rental agreement
        /// </summary>
        /// <remarks>Adds Rental Agreement Time Record</remarks>
        /// <param name="id">id of Rental Agreement to add a time record for</param>
        /// <param name="item">Adds to Rental Agreement Time Record</param>
        /// <response code="200">OK</response>
        IActionResult RentalAgreementsIdTimeRecordsPostAsync(int id, TimeRecord item);

        /// <summary>
        /// Update or create an array of time records associated with a rental agreement
        /// </summary>
        /// <remarks>Update a Rental Agreement&#39;s Time Records</remarks>
        /// <param name="id">id of Rental Agreement to update Time Records for</param>
        /// <param name="items">Array of Rental Agreement Time Records</param>
        /// <response code="200">OK</response>
        IActionResult RentalAgreementsIdTimeRecordsBulkPostAsync(int id, TimeRecord[] items);

        /// <summary>
        /// Get rate records for a rental agreement
        /// </summary>
        /// <remarks>Gets a Rental Agreement&#39;s Time Records</remarks>
        /// <param name="id">id of Rental Agreement to fetch Time Records for</param>
        /// <response code="200">OK</response>
        IActionResult RentalAgreementsIdRentalAgreementRatesGetAsync(int id);

        /// <summary>
        /// Add a rate record to a rental agreement
        /// </summary>
        /// <remarks>Adds Rental Agreement Rate Record</remarks>
        /// <param name="id">id of Rental Agreement to add a rate record for</param>
        /// <param name="item">Adds to Rental Agreement Rate Record</param>
        /// <response code="200">OK</response>
        IActionResult RentalAgreementsIdRentalAgreementRatesPostAsync(int id, RentalAgreementRate item);

        /// <summary>
        /// Update or create an array of rate records associated with a rental agreement
        /// </summary>
        /// <remarks>Update a Rental Agreement&#39;s Time Records</remarks>
        /// <param name="id">id of Rental Agreement to update Time Records for</param>
        /// <param name="items">Array of Rental Agreement Time Records</param>
        /// <response code="200">OK</response>
        IActionResult RentalAgreementsIdRentalAgreementRatesBulkPostAsync(int id, RentalAgreementRate[] items);

        /// <summary>
        /// Get condition records for a rental agreement
        /// </summary>
        /// <remarks>Gets a Rental Agreement&#39;s Time Records</remarks>
        /// <param name="id">id of Rental Agreement to fetch Time Records for</param>
        /// <response code="200">OK</response>
        IActionResult RentalAgreementsIdRentalAgreementConditionsGetAsync(int id);

        /// <summary>
        /// Add a condition record to a rental agreement
        /// </summary>
        /// <remarks>Adds Rental Agreement Condition Record</remarks>
        /// <param name="id">id of Rental Agreement to add a condition record for</param>
        /// <param name="item">Adds to Rental Agreement Condition Record</param>
        /// <response code="200">OK</response>
        IActionResult RentalAgreementsIdRentalAgreementConditionsPostAsync(int id, RentalAgreementCondition item);

        /// <summary>
        /// Update or create an array of condition records associated with a rental agreement
        /// </summary>
        /// <remarks>Update a Rental Agreement&#39;s Time Records</remarks>
        /// <param name="id">id of Rental Agreement to update Time Records for</param>
        /// <param name="items">Array of Rental Agreement Time Records</param>
        /// <response code="200">OK</response>
        IActionResult RentalAgreementsIdRentalAgreementConditionsBulkPostAsync(int id, RentalAgreementCondition[] items);
    }
}
