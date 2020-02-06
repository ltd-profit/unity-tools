﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Omega.Experimental;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Omega.Tools.Tests
{
    public sealed class TransformUtilityTests
    {
        [Test]
        public void GetChildsShouldReturnEmptyArrayFromSystemArrayEmpty()
        {
            var gameObjectInstance = new GameObject();

            var childs = Utilities.Transfrom.GetChilds(gameObjectInstance.transform);

            Assert.NotNull(childs);
            Assert.Zero(childs.Length);

            Assert.AreEqual(childs, Array.Empty<Transform>());
            
            Utilities.Object.AutoDestroy(gameObjectInstance);
        }

        [Test]
        public void GetChildsShouldThrowArgumentNullExceptionWhenParameterIsNull()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => Utilities.Transfrom.GetChilds(null));
        }

        [Test]
        public void GetChildsShouldThrowMissingReferenceExceptionWhenParameterWereDestroyed()
        {
            var gameObjectInstance = new GameObject();

            Utilities.Object.AutoDestroy(gameObjectInstance);

            Assert.Throws<MissingReferenceException>(() =>
                Utilities.Transfrom.GetChilds(gameObjectInstance.transform));
        }
        
        [Test]
        public void GetAllChildsShouldReturnAllChildsTest()
        {
            int goCount = 50; 
            
            var root = new GameObject("root").transform;
            var circuitParent = root;
            var complexHierarchyObjects = GameObjectFactory.New()
                .Custom(go => go.transform.parent = circuitParent)
                .Custom(go => circuitParent = go.transform)
                .Build<Transform>(goCount);

            var result = new List<Transform>(goCount);
            Utilities.Transfrom.GetAllChilds(root, result);
            
            Assert.Zero(complexHierarchyObjects.Except(result).Count());
        }
        
        [Test]
        public void GetAllChildsCountShouldReturnCountAllChildsTest()
        {
            int goCount = 50; 
            
            var root = new GameObject("root").transform;
            var circuitParent = root;
            GameObjectFactory.New()
                .Custom(go => go.transform.parent = circuitParent)
                .Custom(go => circuitParent = go.transform)
                .Build<Transform>(goCount);

            var result = Utilities.Transfrom.GetAllChildsCount(root);
            
            Assert.AreEqual(goCount, result);
        }
    }
}