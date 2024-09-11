using Application.Core.Commands.Offers.CreateOffer;
using Application.Core.Commands.Offers.UpdateOffer;
using Application.Core.DTOs.Offers;
using Application.Core.Queries;
using Application.Core.Queries.Offers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FresherManagement.Api.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OfferController : BaseController
    {
        /// <summary>
        /// Create New Offer.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Description = "Create New Offer.", OperationId = "CreateOffer")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOfferDto request)
        {
            var command = Mapper.Map<CreateOfferCommand>(request);
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Update Offer.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Description = "Update Offer.", OperationId = "UpdateOffer")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateOfferDto request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var command = Mapper.Map<UpdateOfferCommand>(request);
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Get All Offers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Description = "Get All Offers.", OperationId = "GetOffers")]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new Query<List<OfferDto>>()));
        }

        /// <summary>
        /// Get Offer By Id.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Description = "Get Offer By Id.", OperationId = "GetOfferById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await Mediator.Send(new GetByIdQuery<int, OfferDto>(id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Get offers with paging.
        /// </summary>
        /// <returns></returns>
        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Description = "Search Offers With Paging", OperationId = "GetOffersWithPaginationFilter")]
        public async Task<IActionResult> GetWithPaging([FromBody] GetOffersWithPaginationFilterQuery request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}
