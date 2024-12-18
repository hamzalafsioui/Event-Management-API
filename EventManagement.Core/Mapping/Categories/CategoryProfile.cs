﻿using AutoMapper;

namespace EventManagement.Core.Mapping.Categories
{
	public partial class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			GetCategoryListQueryMapping();
			GetCategoryByIdQueryMapping();
			AddCategoryCommandMapping();
			EditCategoryCommandMapping();
		}
	}
}
