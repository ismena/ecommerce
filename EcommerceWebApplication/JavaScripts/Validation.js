var validatedAll = false;

var validated = new Array();

$(document).ready(ValidateAll);

function ValidateAll()
{
	validatedAll = false;
	var i;
	for(i = 1; i <= numberOfArrays; i++) 
	{
		var neededArray = eval("neededVars" + i);
		for (inputId in neededArray)
		{
			input = document.getElementById(inputId);
			if(input != null)
			{
				Validate(input, i);
			}
		}
	}
	validatedAll = true;
}

function Validate(input, arrayNumber)
{
	var neededArray = eval("neededVars" + arrayNumber);
	var submitButton = document.getElementById(eval("buttonId" + arrayNumber));
	
	var validationFunction = eval(neededArray[input.id]);
	
	if (validationFunction(input))
	{
		RemoveError(input);
		if (validated.indexOf(input.id) < 0)
		{
			validated.push(input.id);
		}
	}
	else
	{
		// remove the element if present
		if(validated.indexOf(input.id) >= 0)
		{
			validated.splice(validated.indexOf(input.id), 1);
		}
	}
	
	ArrayValidaton(neededArray, validated, submitButton);
}

function ArrayValidaton(neededArray, validated, submitButton)
{
	var valid = true;
	for (input in neededArray)
	{
		if (validated.indexOf(input) < 0)
		{
			valid = false;
			break;
		}
	}
	
	if (valid)
	{
		submitButton.disabled = false;
	}
	else
	{
		submitButton.disabled = true;
	}
}


function NotEmpty(input)
{
	if(input.value != null && !isNullOrWhiteSpace(input.value))
	{
		return true;
	}

	FireError(input, 'Please fill in the field');	
	return false;
}

function CheckPhone(input)
{
	if(!NotEmpty(input))
	{
		return false;
	}
	for ( var i = 0; i < input.value.length; i++ )
	{
		if (input.value[i] != '-' && 
			input.value[i] != ' ' && 
			input.value[i] != '+' && 
			input.value[i] != '(' &&
			input.value[i] != ')' &&
			input.value[i] != '1' &&
			input.value[i] != '2' &&
			input.value[i] != '3' &&
			input.value[i] != '4' &&
			input.value[i] != '5' &&
			input.value[i] != '6' &&
			input.value[i] != '7' &&
			input.value[i] != '8' &&
			input.value[i] != '9' &&
			input.value[i] != '0')
		{
			FireError(input, 'Only digits and symbols "+,-,( )" are allowed in the field');	
			return false;
		}
	}
	
	return true;
}

function CheckNumber(input)
{
	if(!NotEmpty(input))
	{
		return false;
	}
	for ( var i = 0; i < input.value.length; i++ )
	{
		if (input.value[i] != '1' &&
			input.value[i] != '2' &&
			input.value[i] != '3' &&
			input.value[i] != '4' &&
			input.value[i] != '5' &&
			input.value[i] != '6' &&
			input.value[i] != '7' &&
			input.value[i] != '8' &&
			input.value[i] != '9' &&
			input.value[i] != '0')
		{
			FireError(input, 'Only digits are allowed in this field');	
			return false;
		}
	}
	return true;
}

function RemoveTip(input)
{
	$(input).removeTipTip();
}

function RemoveError(input)
{
	$(input).parent().find('.inputRequirement').remove();
	
	if($(input).parent().find('.checkOk').length == 0)
	{
		$(input).parent().append('<span class="checkOk"></span>');
	}
	
	$(input).removeTipTip();
}

function FireError(input, error)
{
	$(input).parent().find('.checkOk').remove();

	
	
	if(validatedAll)
	{
		$(input).tipTip({content: error});
	}
}


function isNullOrWhiteSpace(str)
{
    return str === null || str.match(/^ *$/) !== null;
}