using System;
using HappyPenguin.Controllers;

namespace HappyPenguin.UI
{
	public abstract class UIElementController<T> : Controller<UIElementBehaviour<T>>
	{
	}
}

