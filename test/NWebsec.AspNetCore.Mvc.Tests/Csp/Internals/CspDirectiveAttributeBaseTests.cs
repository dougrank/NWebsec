﻿// Copyright (c) André N. Klingsheim. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Moq;
using NWebsec.AspNetCore.Mvc.Csp;
using NWebsec.AspNetCore.Mvc.Csp.Internals;
using Xunit;

namespace NWebsec.AspNetCore.Mvc.Tests.Csp.Internals
{
    public class CspDirectiveAttributeBaseTests
    {

        [Fact]
        public void ValidateParams_EnabledAndNoDirectives_ThrowsException()
        {
            var cspSandboxAttributeBaseMock = new Mock<CspDirectiveAttributeBase>(MockBehavior.Strict).Object;
            Assert.Throws<ArgumentException>(() => cspSandboxAttributeBaseMock.ValidateParams());
        }

        [Fact]
        public void ValidateParams_DisabledAndNoDirectives_NoException()
        {
            var cspSandboxAttributeBaseMock = new Mock<CspDirectiveAttributeBase>(MockBehavior.Strict).Object;
            cspSandboxAttributeBaseMock.Enabled = false;

            cspSandboxAttributeBaseMock.ValidateParams();
        }

        [Fact]
        public void ValidateParams_EnabledAndDirectives_NoException()
        {
            foreach (var cspSandboxAttributeBase in ConfiguredAttributes())
            {
                cspSandboxAttributeBase.ValidateParams();
            }
        }

        [Fact]
        public void ValidateParams_EnabledAndMalconfiguredDirectives_NoException()
        {
            foreach (var cspSandboxAttributeBase in MalconfiguredAttributes())
            {
                Assert.Throws<ArgumentException>(() => cspSandboxAttributeBase.ValidateParams());
            }
        }

        private IEnumerable<CspDirectiveAttributeBase> ConfiguredAttributes()
        {
            var attribute = new Mock<CspDirectiveAttributeBase>(MockBehavior.Strict).Object;
            attribute.None = true;
            yield return attribute;

            attribute = new Mock<CspDirectiveAttributeBase>(MockBehavior.Strict).Object;
            attribute.Self = true;
            yield return attribute;

            yield return new CspStyleSrcAttribute { UnsafeInline = true };
            yield return new CspScriptSrcAttribute { UnsafeEval = true };
            yield return new CspScriptSrcAttribute { StrictDynamic = true };
        }

        private IEnumerable<CspDirectiveAttributeBase> MalconfiguredAttributes()
        {
            var attribute = new Mock<CspDirectiveAttributeBase>(MockBehavior.Strict).Object;
            attribute.Self = true;
            attribute.None = true;
            yield return attribute;

            attribute = new Mock<CspDirectiveAttributeBase>(MockBehavior.Strict).Object;
            attribute.None = true;
            attribute.CustomSources = "www.nwebsec.com";
            yield return attribute;

            yield return new CspStyleSrcAttribute { None = true, UnsafeInline = true };
            yield return new CspScriptSrcAttribute { None = true, UnsafeEval = true };
            yield return new CspScriptSrcAttribute { None = true, StrictDynamic = true };
        }
    }
}