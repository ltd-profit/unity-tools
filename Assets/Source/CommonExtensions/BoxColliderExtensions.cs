﻿using System;
using Omega.Tools;
using UnityEngine;

public static class BoxColliderExtensions
{
    /// <summary>
    /// Устанавливает центр и размер коллайдеру по указанным границам  
    /// </summary>
    /// <param name="boxCollider">Коллайдер</param>
    /// <param name="bounds">Границы для коллайдера</param>
    /// <exception cref="NullReferenceException">Параметр <param name="boxCollider"/>>указывает на null</exception>
    /// <exception cref="MissingReferenceException">Параметр <param name="boxCollider"/>>указывает на уничтоженный объект</exception>
    public static void SetBounds(this BoxCollider boxCollider, Bounds bounds)
    {
        if (ReferenceEquals(boxCollider, null))
            throw new NullReferenceException(nameof(boxCollider));
        if (!boxCollider)
            throw new MissingReferenceException(nameof(boxCollider));

        BoxColliderUtility.SetAsBoundsWithoutChecks(boxCollider, bounds);
    }
}