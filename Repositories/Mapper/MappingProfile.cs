﻿using AutoMapper;
using Models;
using ViewModels.DTO;
using ViewModels.Input;
using System;

namespace Repositories.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			//Models -> DTOs
			CreateMap<Project, ProjectDTO>();
			CreateMap<Role, RoleDTO>();
			CreateMap<Team, TeamDTO>();
			CreateMap<User, UserDTO>();
			CreateMap<Vacation, VacationDTO>();

			//ViewModels -> Models
			CreateMap<ProjectViewModel, Project>();
			CreateMap<RoleViewModel, Role>();
			CreateMap<TeamViewModel, Team>();
			CreateMap<RegisterUserViewModel, User>();
			CreateMap<VacationViewModel, Vacation>()
				.ForMember(vacation => vacation.CreationDate, opt => opt.MapFrom(src => DateTime.Now));
		}
	}
}
