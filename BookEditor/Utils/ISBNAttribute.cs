using System;
using System.ComponentModel.DataAnnotations;

namespace BookEditor
{
	// ReSharper disable once InconsistentNaming
	[AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
	public sealed class ISBNAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			return true;
		}
	}
}
