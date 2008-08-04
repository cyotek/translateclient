

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
