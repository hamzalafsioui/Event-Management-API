using EventManagement.Data.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Data.Entities.Identity
{
	public class UserRefreshToken:IHasCreatedAt,IHasExpiredAt
	{
		[Key]
		public int Id { get; set; }
		public int UserId { get; set; }
		public string? Token { get; set; }
		public string? RefreshToken { get; set; }
		public string? JwtId { get; set; }
		public bool IsUsed { get; set; }
		public bool IsRevoked { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ExpiredAt { get; set; }
		[ForeignKey(nameof(UserId))]
		[InverseProperty(nameof(user.UserRefreshTokens))]
		public virtual User user { get; set; }
	}
}
