HanaFractal_WindowsForms
========================
HANA Fractal Viewer implemented in C# and .NET 4.

To get it running, open solution, navigate to project FractalViewerWindows and edit HanaServerDetails.resx with your server, login details and OData service name. Then build and execute.

Projects
--------
The projects contained in the solution are:

* **FractalModel** - Makes OData calls to HANA and parses results.
* **FractalViewerWindows** - Draws results obtained from the FractalModel project. Knows nothing about HANA.
* **Newtonsoft.Json** - Implements JSON parsing for .NET (or you could use the standard JsonObject class if you're using .NET 4.5 or above).

Improvements
------------
* The client uses the feeble default WinForms drawing which is very slow.  Much better would be a DirectX client.  This would mean a new client project would connect the same backend FractalModel project.
* Client could caches result and zoom in with transitions.

