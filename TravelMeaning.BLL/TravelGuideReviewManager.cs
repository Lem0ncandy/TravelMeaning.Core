﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelMeaning.IBLL;
using TravelMeaning.IDAL;
using TravelMeaning.Models.DTO;
using TravelMeaning.Models.Model;

namespace TravelMeaning.BLL
{
    public class TravelGuideReviewManager : ITravelGuideReviewManager
    {
        protected readonly IMapper _mapper;
        protected readonly ITravelGuideService _travelGuideSvc;
        protected readonly ITravelGuideReviewService _guideReviewService;

        public TravelGuideReviewManager(IMapper mapper, ITravelGuideService travelGuideSvc, ITravelGuideReviewService reviewService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _travelGuideSvc = travelGuideSvc ?? throw new ArgumentNullException(nameof(travelGuideSvc));
            _guideReviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
        }

        public async Task<bool> CreateGuideReview(Guid guideId)
        {
            var guideReview = new TravelGuideReview
            {
                TravelGuideId = guideId,
            };
            return await _guideReviewService.CreateAsync(guideReview);
        }

        public async Task<List<GuideReviewDTO>> GetAllGuideReview(int page, int size)
        {
            //var list = await _reviewService.GetAll().Skip(page).Take(size).Include(x => x.TravelGuide).ToListAsync();
            var list = await _travelGuideSvc.GetAll().Skip(page).Take(size).Include(x => x.TravelGuideReview).Include(x => x.User).ToListAsync();
            return _mapper.Map<List<GuideReviewDTO>>(list);
        }

        public async Task<List<GuideReviewDTO>> GetAllGuideReviewByState(int page, int size, ReviewState state)
        {
            var list = await _travelGuideSvc.GetAll().Include(x => x.TravelGuideReview).Where(x => x.TravelGuideReview.State == state).Include(x => x.User).Skip(page).Take(size).ToListAsync();
            return _mapper.Map<List<GuideReviewDTO>>(list);
        }

        public async Task<int> GetAllGuideReviewCount()
        {
            return await _travelGuideSvc.GetAll().CountAsync();
        }

        public async Task<bool> ModiflyNoteByGuideId(Guid guideId, string content)
        {
            var review = _guideReviewService.GetAll().Where(x => x.TravelGuideId == guideId).FirstOrDefault();
            review.Note = content;
            return await _guideReviewService.EditAsync(review);
        }

        public async Task<bool> ModiflyNoteById(Guid id, string content)
        {
            var review = await _guideReviewService.GetOneByIdAsync(id);
            review.Note = content;
            return await _guideReviewService.EditAsync(review);
        }

        public async Task<bool> ModifyReviewStateByGuideId(Guid guideId, ReviewState state)
        {
            var guideReview = await _guideReviewService.GetAll().Where(x => x.TravelGuideId == guideId).FirstOrDefaultAsync();
            guideReview.State = state;
            return await _guideReviewService.EditAsync(guideReview);
        }

        public async Task<bool> ModifyReviewStateById(Guid id, ReviewState state)
        {
            var guideReview = await _guideReviewService.GetAll().Where(x => x.Id == id).FirstOrDefaultAsync();
            guideReview.State = state;
            return await _guideReviewService.EditAsync(guideReview);
        }
    }
}
