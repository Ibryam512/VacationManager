﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models;
using ViewModels.Input;
using Web.Services.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Models.SearchModel;

namespace Web.Controllers
{
	public class TeamController : Controller
	{

		private IMapper _mapper;
		private ITeamService _teamService;
		private IUserService _userService;
		private IProjectService _projectService;
		public TeamController(IMapper mapper, ITeamService teamService, IUserService userService, IProjectService projectService)
		{
			this._mapper = mapper;
			this._teamService = teamService;
			this._userService = userService;
			this._projectService = projectService;
		}

		#region CreateTeam


		[HttpGet]
		public IActionResult Index()
		{
			TeamSearch search = new TeamSearch();

			search.Results = this._teamService.GetTeams();
			search.Results.ForEach(x => x.TeamLeader = this._userService.GetUser(x.TeamLeaderId));

			return View(search);
		}

		[HttpGet]
		public IActionResult Search(TeamSearch search)
		{
			search.Results = this._teamService.GetTeams();
			search.Results.ForEach(x => x.TeamLeader = this._userService.GetUser(x.TeamLeaderId));

			if (search.Name is not null)
			{
				search.Results = search.Results.Where(x => x.Name.Contains(search.Name)).ToList();
			}
			if (search.TeamLeadNames is not null)
			{
				search.Results = search.Results.Where(x => x.TeamLeader.FirstName.Contains(search.TeamLeadNames) || x.TeamLeader.LastName.Contains(search.TeamLeadNames)).ToList();
			}

			return View("Index", search);
		}

		[HttpGet]
		public IActionResult Create()
		{
			TeamViewModel model = new TeamViewModel();
			return View(model);
		}

		[HttpPost]
		public IActionResult Create(TeamViewModel model)
		{
			if (ModelState.IsValid)
			{
				Team team = this._mapper.Map<Team>(model);
				List<User> teamMembers = new List<User>();
				User teamLeader = _userService.GetUsers().FirstOrDefault(x => x.UserName == model.TeamLeaderUsername);
				foreach (string username in model.DevelopersUsernames)
				{
					teamMembers.Add(_userService.GetUsers().FirstOrDefault(x => x.UserName == username));
				}
				team.TeamLeader = teamLeader;
				team.Developers = teamMembers;
				team.Project = _projectService.GetProjects().FirstOrDefault(x => x.Name == model.ProjectName);
				_teamService.AddTeam(team);
				return RedirectToAction("Index", "Team");
			}
			else
			{
				return View(model);
			}
		}
		#endregion

		#region DeleteTeam
		[HttpGet]
		[Route("team/delete/{id}")]
		public IActionResult Delete([FromRoute] string id)
		{
			if (id is null)
			{
				return NotFound();
			}
			Team deleteTeam = _teamService.GetTeam(id);
			if (deleteTeam is null)
			{
				return NotFound();
			}
			_teamService.DeleteTeam(deleteTeam);
			return RedirectToAction("Index", "Team");

		}
		#endregion
	}
}
