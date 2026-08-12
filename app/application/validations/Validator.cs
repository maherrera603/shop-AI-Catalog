using System.Text.RegularExpressions;

namespace Catalog.Api.app.application.validations;


public class Validator {

	
	public static bool Text(string text){
		return Regex.IsMatch(text.Trim(), @"^[A-Za-zÀ-ÿ ]+$");
	}


	public static bool TextSlug(string text){
		return Regex.IsMatch(text.Trim(), @"^[a-z0-9]+(?:-[a-z0-9]+)*$");
	}


	public static bool TextDescription(string text){
		return Regex.IsMatch(text.Trim(), @"^[A-Za-zÀ-ÿ0-9\s.,;:()/%\-]+$");
	}


	public static bool Slug(string text)
	{
		return Regex.IsMatch(text, @"^[a-z0-9]+(?:-[a-z0-9]+)*$");
	}

	public static bool Sku(string text)
	{
		return Regex.IsMatch(text, @"^[A-Za-z0-9-]+$");
	}

	public static bool ProductName(string text)
	{
		return Regex.IsMatch( text.Trim(), 	@"^[A-Za-zÀ-ÿ0-9 -]+$" );
	}
}
