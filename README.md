# Unity Development Toolkit 2
Offering a comprehensive collection of practices and tools designed to streamline and enhance the game development process within the Unity engine.

## Installation
Install the Unity Package from the GitHub repository URL

1. Copy the Git URL above
2. Open the Unity Package Manager
3. Click the plus button
4. Click "Add package from Git URL"
5. Paste, and click Add.

## Pinacles of Design
- Modular Runtime Singletons with State Management 
  -
  The `Runtime<TRuntime, TData>` classes are modular singletons instantiated at the start of the runtime in Unity. These classes provide a versatile foundation for managing game objects, input, and instances. They also incorporate nested `IStateMachine` for dynamic game state management, enabling developers to create organized and efficient Unity projects while seamlessly handling state transitions and interactions.
```c#
// This demonstrates creating a simple game Runtime by extending from the Runtime class.

using UnityEngine;
using Rich.System;
using Rich.Controllables;
using Rich.StateMachines;

public class MyGame : Runtime<MyGame, MyGameData>
{
  private float gameTime = 0.0f;
  private float spawnInterval = 2.0f;
  private float nextSpawnTime = 0.0f;
  
  public class Title : State<MyGame>
  {
    //Add Title Screen Functionality
  
    public void OnEnterPlayMode()
    {
      SetState<Playing>();
    }
  }
  
  public class Playing : State<MyGame>
  {
    public int score = 0;
  
    public override void OnEnter()
    {
        // Initialize game-specific data.
        Context.score = 0;
    }
  
    public override void OnUpdate()
    {
        // Game loop logic.
        gameTime += Time.deltaTime;
  
        if (gameTime >= nextSpawnTime)
        {
            // Spawn game objects or perform game events here.
            SpawnEnemy();
            nextSpawnTime = gameTime + spawnInterval;
        }
  
        // Handle user input or game events.
        if (ControllerManager.IsInputDown("Jump"))
        {
            // Perform a game action, e.g., player jump.
            Jump();
        }
    }
  
    private void SpawnEnemy()
    {
        // Implement enemy spawning logic here.
        Debug.Log("Enemy spawned!");
    }
  
    private void Jump()
    {
        // Implement player jump logic here.
        Debug.Log("Player jumped!");
    }
  }
}
```

- Modular Architecture
  -
  At its core, the toolkit embraces a modular architecture, allowing developers to build and extend their games in a structured and organized manner.
- Interface-Driven Design
  -
  The toolkit promotes an interface-driven design approach, ensuring clear contracts between components, and facilitating seamless integration of custom features.
  - **Streamlined Integration with Managers**: Interfaces within the Unity Toolkit serve as powerful contracts that define the essential behavior and properties required for integration with various managers, such as the `StateMachineManager`. These interfaces eliminate the need for classes to implement specific members, simplifying the integration process.
  - **Standardized Access with Extension Methods**: Through the use of standard implementations and extension methods, classes that implement these interfaces gain instant access to manager functionalities. For instance, the `IStateMachine` interface defines properties like `stateReferences` and `CurrentState`, and by implementing this interface, classes automatically gain access to these properties without the need for manual implementation.
  - **Seamless State Management**: In the example of the `IStateMachine` interface, classes implementing it can effortlessly manage their states, retrieve state references, and switch between states using the provided properties, reducing development overhead and promoting a more organized and modular approach to state management within Unity projects.
```c#
// This is a demonstration of what it takes to create a State Machine enabled MonoBehaviour. 
// This example also uses the GetInstance() extension method to get the UDT Instance associated with this GameObject, 
// so we can more easily access it's speed Property.

public class Movement : MonoBehaviour, IStateMachine
{
  public class Idle : State<Movement>
  {
    public override void OnUpdate()
    {
      if(Context.GetInstance().speed > 0)
        SetState<Walk>(); 
    }
  }
  public class Walk : State<Movement>
  {
    public override void OnUpdate()
    {
      if(Context.GetInstance().speed == 0)
        SetState<Idle>(); 
    }
  }
}
```
- Centralized Managers
  -
  It introduces central managers, such as the StateMachineManager, ControllerManager, and ObjectPoolManager, to handle complex tasks and enhance project management.
- GameObject-Centric Philosophy
  -
  Rooted in Unity's GameObject concept, the toolkit simplifies game object management, enabling developers to focus on game logic and design.
- Event-Driven Interactivity
  -
  Event-driven workflows enhance interactivity by allowing dynamic responses to player actions and events.
- Input System Integration
  -
  Integration with Unity's Input System provides robust and configurable input management for responsive gameplay.
- Dynamic State Handling
  -
  The StateMachineManager offers dynamic state management, catering to complex and evolving game behavior.
- Singleton Pattern
  -
  The toolkit employs the Singleton pattern to ensure single instances of essential components, promoting project-wide consistency.
- Resource-Driven Object Pooling
  -
  Object pooling with the ObjectPoolManager utilizes resources and prefabs, optimizing performance and resource management.
- Instance-Based Game Object Interaction
  -
  The Instances framework simplifies game object interaction, offering a user-friendly approach to manipulation and control.
  - **Component Management**: Instances support the addition and removal of components, allowing developers to tailor game objects to their needs.
- Simplified Game Development
  -
  By adhering to these practices and using the toolkit's features, developers can create Unity games efficiently, with cleaner code and a more organized project structure.

The Unity Development Toolkit empowers developers with a set of best practices and modular tools, fostering a more efficient, organized, and user-friendly approach to Unity game development.
