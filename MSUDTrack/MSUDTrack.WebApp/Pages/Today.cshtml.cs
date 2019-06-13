﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSUDTrack.DataModels.Models;
using MSUDTrack.Services;
using MSUDTrack.Services.DTOs;

namespace MSUDTrack.WebApp.Pages
{
    public class TodayModel : PageModel
    {
        private readonly RecordsService _recordsService;
        private readonly PeriodsService _periodsService;
        private readonly FoodsService _foodsService;

        public TodayModel(RecordsService recordsService, PeriodsService periodsService, FoodsService foodsService)
        {
            _recordsService = recordsService;
            _periodsService = periodsService;
            _foodsService = foodsService;
        }

        public TodayDTO TodaysLog { get; set; } = new TodayDTO();

        public List<Food> Foods { get; set; }

        public async Task OnGetAsync()
        {
            var periods = await _periodsService.ListAsync();

            foreach (var period in periods)
            {
                TodaysLog.Periods.Add(new PeriodDTO()
                {
                    Period = period,
                    Records = await _recordsService.GetRecordsByPeriodAsync(period.Id)
                });
            }

            Foods = await _foodsService.ListAsync();
        }
    }

    public class TodayDTO
    {
        public List<PeriodDTO> Periods { get; set; } = new List<PeriodDTO>();
    }

    public class PeriodDTO
    {
        public Period Period = new Period();

        public List<Record> Records { get; set; }
    }
}
