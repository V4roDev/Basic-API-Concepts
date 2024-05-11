using System;
using Backend.DTOs;

namespace Backend.Services
{
	public interface IPostService
	{

		public Task<IEnumerable<PostDTO>> Get();

	}
}

