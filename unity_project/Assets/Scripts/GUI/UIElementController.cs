using System;
using Pux.Controllers;

namespace Pux.UI
{
	public abstract class UIElementController<T> : Controller<UIElementBehaviour<T>>
	{
	}
}

