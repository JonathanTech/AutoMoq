﻿using AutoMoq.Helpers;
using Moq;
using NUnit.Framework;
using Should;

namespace AutoMoq.Tests
{
    [TestFixture]
    public class with_automoqer_Tests
    {
        private AutoMoqer mocker;

        [SetUp]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [Test]
        public void Instantiating_the_automoqer_sets_the_static_mocker()
        {
            with_automoqer.mocker = null;
            var test = new with_automoqer();
            with_automoqer.mocker.ShouldNotBeNull();
        }

        [Test]
        public void GetMock_returns_the_mock()
        {
            var test = new with_automoqer();
            with_automoqer.mocker = new AutoMoqer();

            with_automoqer.GetMock<IDependency>().ShouldBeSameAs(with_automoqer.mocker.GetMock<IDependency>());
        }

        [Test]
        public void Create_returns_the_class_resolved_from_automoqer()
        {
            var test = new with_automoqer();
            with_automoqer.mocker = new AutoMoqer();

            with_automoqer.Create<Test>()
                .Dependency.ShouldBeSameAs(with_automoqer.GetMock<IDependency>().Object);
        }

        [Test]
        public void SetInstance_sets_the_instance()
        {
            with_automoqer.mocker = new AutoMoqer();

            var instance = new Mock<IDependency>().Object;
            with_automoqer.SetInstance(instance);

            with_automoqer.Create<Test>().Dependency.ShouldBeSameAs(instance);
        }

        public interface IDependency
        {
        }

        public class Test
        {
            public readonly IDependency Dependency;

            public Test(IDependency Dependency)
            {
                this.Dependency = Dependency;
            }
        }
    }
}