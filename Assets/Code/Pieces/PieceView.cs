﻿using DefaultEcs;
using DG.Tweening;
using UnityEngine;

public class PieceView : View
{
    public SpriteRenderer Sprite;
    public float DestroyDuration;

    public override void Link(Entity entity)
    {
        base.Link(entity);

        if (_linkedEntity.Get<Piece>().Type >= 0)
        {
            var config = _linkedEntity.World.Get<IPieceColorsConfig>();
            Sprite.color = config.Colors[_linkedEntity.Get<Piece>().Type];
        }
    }

    //public override void OnPosition(GameEntity entity, Vector2Int value)
    //{
    //    transform.DOKill();
    //    var isTopRow = value.y == Contexts.sharedInstance.game.board.Size.y - 1;
    //    if (isTopRow)
    //        transform.localPosition = new Vector3(value.x, value.y + 1);

    //    transform.DOMove(new Vector3(value.x, value.y, 0f), 0.3f);
    //}

    //protected override void OnDestroy()
    //{
    //    var color = Sprite.color;
    //    color.a = 0f;
    //    Sprite.material.DOColor(color, DestroyDuration);
    //    gameObject.transform
    //        .DOScale(Vector3.one * 1.5f, DestroyDuration)
    //        .OnComplete(base.OnDestroy);
    //}
}
