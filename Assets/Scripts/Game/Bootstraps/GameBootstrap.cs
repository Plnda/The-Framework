using System;
using Bootstrap;
using Framework.Levels;
using UnityEngine;

namespace Game.Scenes
{
    public class GameBootstrap: BasicBootstrap
    {
        [SerializeField] private Level _hudLevel;
        
        protected override async void OnStart()
        {
            base.OnStart();

            await _hudLevel.Load(false);
        }
    }
}