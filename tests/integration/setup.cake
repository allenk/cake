// Utilities
#load "./utilities/paths.cake"
#load "./utilities/xunit.cake"
#load "./utilities/context.cake"

//////////////////////////////////////////////////
// SETUP
//////////////////////////////////////////////////

Setup<ScriptContext>(setupContext =>
{
    // Output information from setup task
    setupContext.Log.Information(
        Verbosity.Quiet,
        "Performing setup initated by {0} ({1} tasks to be executed beginning with {2})",
        setupContext.TargetTask?.Name,
        setupContext.TasksToExecute?.Count,
        setupContext.TasksToExecute?.Select(task => task.Name).FirstOrDefault()
        );

    // Perform artifact cleanup
    CleanDirectory(Paths.Temp);

    // Create a new script context.
    return new ScriptContext(true);
});

//////////////////////////////////////////////////
// TESTS
//////////////////////////////////////////////////

Task("Can-Access-Typed-Data")
    .Does<ScriptContext>(data => 
{
    Assert.True(data.Initialized);
});

Task("Can-Access-Typed-Data-Async")
    .Does<ScriptContext>(async data => 
{
    await System.Threading.Tasks.Task.Delay(0);
});

Task("Can-Access-Typed-Data-With-Context")
    .Does<ScriptContext>((data, context) => 
{
    Assert.True(data.Initialized);
});

Task("Can-Access-Typed-Data-With-Context-Async")
    .Does<ScriptContext>(async (data, context) => 
{
    await System.Threading.Tasks.Task.Delay(0);
});

Task("Can-Access-Typed-Data-WithCriteria-True")
    .WithCriteria<ScriptContext>((data, context) => data.Initialized)
    .Does<ScriptContext>(data => 
{
    Assert.True(data.Initialized);
});

Task("Can-Access-Typed-Data-WithCriteria-False-Message")
    .WithCriteria<ScriptContext>((data, context) => !data.Initialized, "Should only run if not initialized.")
    .Does<ScriptContext>(data => 
{
    Assert.False(data.Initialized);
});

//////////////////////////////////////////////////
// TARGETS
//////////////////////////////////////////////////

Task("Setup-Tests")
    .IsDependentOn("Can-Access-Typed-Data")
    .IsDependentOn("Can-Access-Typed-Data-Async")
    .IsDependentOn("Can-Access-Typed-Data-With-Context")
    .IsDependentOn("Can-Access-Typed-Data-With-Context-Async")
    .IsDependentOn("Can-Access-Typed-Data-WithCriteria-True")
    .IsDependentOn("Can-Access-Typed-Data-WithCriteria-False-Message");