using Framework.Levels;

namespace Bootstrap
{
    public class NavigationBootstrap: BasicBootstrap
    {
        public Level Destination;
        
        public async void MoveToDestinationLevel()
        {
            await Destination.Load();
            await UnloadCurrentLevel();
        }
    }
}