using Zenject;
using UnityEngine;
using Player.Controller;

public class SpawnInstaller : MonoInstaller
{
    [SerializeField] private PlayerMover _player;
    [SerializeField] private Transform _playerSpawnpoint;

    public override void InstallBindings()
    {
        var player = Container.InstantiatePrefabForComponent<PlayerMover>(_player, _playerSpawnpoint);

        Container.Bind<PlayerMover>().FromInstance(player).AsSingle();
    }
}
