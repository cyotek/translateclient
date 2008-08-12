

//Remove all childs from the element
function RemoveAllChilds(parentName)
{
	var element = document.getElementById(parentName);
	if(element)
	{
		while (element.firstChild) 
		{
	  		element.removeChild(element.firstChild);
		}
		return true;
	}
	return false;
}

//remove element from document
function RemoveElement(name)
{
	var element = document.getElementById(name);
	if(element)
	{
		var parent = element.parentNode;
		if(parent)
		{
	  		parent.removeChild(element);
	  		return true;
		}
	}
	return false;
}

function GetCurrentSelection() 
{
	if (document.getSelection) 
	{
		var str = document.getSelection();
		if (window.RegExp) 
		{
			var regstr = unescape("%20%20%20%20%20");
			var regexp = new RegExp(regstr, "g");
			str = str.replace(regexp, "");
		}
	} 
	else if (document.selection && document.selection.createRange) 
	{
		var range = document.selection.createRange();
		var str = range.text;
	} 
	else
	{
		var str = "";//"Sorry, this is not possible with your browser.";
	}
	return str;
}

