﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelMeaning.IBLL;
using TravelMeaning.Models.DTO;
using TravelMeaning.Models.Model;
using TravelMeaning.Models.ResponseModels;
using TravelMeaning.Models.ResponseModels.GuideReview;
using TravelMeaning.Models.ViewModels.Review;

namespace TravelMeaning.Web.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize(policy: "Review")]
    //[Authorize(policy: "Editor")]
    [ApiController]
    public class GuideReviewController : ControllerBase
    {
        private readonly ITravelGuideReviewManager _reviewManager;

        public GuideReviewController(ITravelGuideReviewManager reviewManager)
        {
            _reviewManager = reviewManager ?? throw new ArgumentNullException(nameof(reviewManager));
        }

        [HttpGet]
        public async Task<ResponseModel<GuideReviewListModel>> Get(int page, int size, string state)
        {
            var responseModel = new ResponseModel<GuideReviewListModel>
            {
                Data = new GuideReviewListModel()
            };
            page = page <= 0 ? 0 : page - 1;
            size = size < 0 ? 0 : size;
            responseModel.Data.Guides = await _reviewManager.GetAllGuideReview(page,size);
            responseModel.Data.Count = await _reviewManager.GetAllGuideReviewCount();
            responseModel.Code = StateCode.Sucess;
            return responseModel;
        }

        // PUT: api/Review/5
        [HttpPut("[action]")]
        public async Task<ResponseModel<GenericModel>> ModifyState(ModifyStateViewModel viewModel)
        {
            var responseModel = new ResponseModel<GenericModel>
            {
                Data = new GenericModel()
            };
            if (Guid.TryParse(viewModel.Id, out Guid guideId) && Enum.TryParse<ReviewState>(viewModel.State.ToString(), out ReviewState reviewState))
            {
                 responseModel.Data.IsSucess = await _reviewManager.ModifyReviewStateByGuideId(guideId, reviewState);
                responseModel.Code = StateCode.Sucess;
            }
            return responseModel;
        }
        [HttpPut("[action]")]
        public async Task<ResponseModel<GenericModel>> ModifyNote(ModifyNoteViewModel viewModel)
        {
            var responseModel = new ResponseModel<GenericModel>
            {
                Data = new GenericModel()
            };
            if (Guid.TryParse(viewModel.Id, out Guid guideId))
            {
                responseModel.Data.IsSucess = await _reviewManager.ModiflyNoteByGuideId(guideId, viewModel.Note);
                responseModel.Code = StateCode.Sucess;
            }
            return responseModel;

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
