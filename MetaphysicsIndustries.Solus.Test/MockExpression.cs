using System;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Test
{
    class MockExpression : Expression
    {
        public MockExpression(
            Func<SolusEnvironment, IMathObject> evalf=null,
            Func<Expression> cloneF=null,
            Action<IExpressionVisitor> acceptVisitorF=null,
            Func<SolusEnvironment, bool> isResultScalarF=null,
            Func<SolusEnvironment, bool> isResultVectorF=null,
            Func<SolusEnvironment, bool> isResultMatrixF=null,
            Func<SolusEnvironment, int> getResultTensorRankF=null,
            Func<SolusEnvironment, bool> isResultStringF=null,
            Func<SolusEnvironment, int, int> getResultDimensionF=null,
            Func<SolusEnvironment, int[]> getResultDimensionsF=null,
            Func<SolusEnvironment, int> getResultVectorLengthF=null,
            Func<SolusEnvironment, int> getResultStringLengthF=null)
        {
            EvalF = evalf;
            CloneF = cloneF;
            AcceptVisitorF = acceptVisitorF;
            IsResultScalarF = isResultScalarF;
            IsResultVectorF = isResultVectorF;
            IsResultMatrixF = isResultMatrixF;
            GetResultTensorRankF = getResultTensorRankF;
            IsResultStringF = isResultStringF;
            GetResultDimensionF = getResultDimensionF;
            GetResultDimensionsF = getResultDimensionsF;
            GetResultVectorLengthF = getResultVectorLengthF;
            GetResultStringLengthF = getResultStringLengthF;
        }

        public Func<SolusEnvironment, IMathObject> EvalF;
        public override IMathObject Eval(SolusEnvironment env)
        {
            if (EvalF != null) return EvalF(env);
            throw new System.NotImplementedException();
        }

        public Func<Expression> CloneF;
        public override Expression Clone()
        {
            if (CloneF != null) return CloneF();
            throw new NotImplementedException();
        }

        public Action<IExpressionVisitor> AcceptVisitorF;
        public override void AcceptVisitor(IExpressionVisitor visitor)
        {
            if (CloneF != null)
            {
                AcceptVisitorF(visitor);
                return;
            }

            throw new NotImplementedException();
        }

        public Func<SolusEnvironment, bool> IsResultScalarF;
        public override bool IsResultScalar(SolusEnvironment env)
        {
            if (IsResultScalarF != null) return IsResultScalarF(env);
            throw new NotImplementedException();
        }

        public Func<SolusEnvironment, bool> IsResultVectorF;
        public override bool IsResultVector(SolusEnvironment env)
        {
            if (IsResultVectorF != null) return IsResultVectorF(env);
            throw new NotImplementedException();
        }

        public Func<SolusEnvironment, bool> IsResultMatrixF;
        public override bool IsResultMatrix(SolusEnvironment env)
        {
            if (IsResultMatrixF != null) return IsResultMatrixF(env);
            throw new NotImplementedException();
        }

        public Func<SolusEnvironment, int> GetResultTensorRankF;
        public override int GetResultTensorRank(SolusEnvironment env)
        {
            if (GetResultTensorRankF != null) return GetResultTensorRankF(env);
            throw new NotImplementedException();
        }

        public Func<SolusEnvironment, bool> IsResultStringF;
        public override bool IsResultString(SolusEnvironment env) 
        {
            if (IsResultStringF != null) return IsResultStringF(env);
            throw new NotImplementedException();
        }

        public Func<SolusEnvironment, int, int> GetResultDimensionF;
        public override int GetResultDimension(SolusEnvironment env, int index)
        {
            if (GetResultDimensionF != null) return GetResultDimensionF(env, index);
            throw new NotImplementedException();
        }

        public Func<SolusEnvironment, int[]> GetResultDimensionsF;
        public override int[] GetResultDimensions(SolusEnvironment env)
        {
            if (GetResultDimensionsF != null) return GetResultDimensionsF(env);
            throw new NotImplementedException();
        }

        public Func<SolusEnvironment, int> GetResultVectorLengthF;
        public override int GetResultVectorLength(SolusEnvironment env)
        {
            if (GetResultVectorLengthF != null) return GetResultVectorLengthF(env);
            throw new NotImplementedException();
        }

        public Func<SolusEnvironment, int> GetResultStringLengthF;
        public override int GetResultStringLength(SolusEnvironment env)
        {
            if (GetResultStringLengthF != null) return GetResultStringLengthF(env);
            throw new NotImplementedException();
        }
    }
}