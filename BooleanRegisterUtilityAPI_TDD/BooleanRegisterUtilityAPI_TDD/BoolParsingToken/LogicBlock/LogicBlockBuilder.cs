using BooleanRegisterUtilityAPI_TDD.LogicBlockOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterUtilityAPI_TDD.BoolParsingToken.LogicBlock
{
    public class LogicBlockBuilder
    {

        public LogicBlock m_current;

        public LogicBlockBuilder(LogicBlock logic)
        {
            m_current = logic;
        }



        public LogicBlockBuilder()
        {
            m_current = null;
        }


        public LogicBlockBuilder Append(AppendSide side, AppendGroupType appendType, params LogicBlock[] logic)
        {
            return Append(side == AppendSide.Left, appendType, logic);

        }
        public LogicBlockBuilder Append(bool sideLeft, AppendGroupType appendType, params LogicBlock[] logic)
        {

            List<LogicBlock> tmp = new List<LogicBlock>();
            if (m_current != null)
                tmp.Add(m_current);

            if (sideLeft)
            {
                tmp.InsertRange(0, logic);
            }
            else
            {

                tmp.AddRange(logic);
            }


            switch (appendType)
            {
                case AppendGroupType.And:
                    m_current = new AndLogic(tmp.ToArray());
                    break;
                case AppendGroupType.or:
                    m_current = new OrLogic(tmp.ToArray());
                    break;
                case AppendGroupType.Xor:
                    m_current = new XorLogic(tmp.ToArray());
                    break;
                case AppendGroupType.Eqv:
                    m_current = new EquivalentLogic(tmp.ToArray());
                    break;
                default:
                    break;
            }

            return this;
        }

        public LogicBlockBuilder Append(AppendSide side, AppendDuoType appendType, LogicBlock logic)
        {
            return Append(side == AppendSide.Left, appendType, logic);

        }
        public LogicBlockBuilder Append(bool sideLeft, AppendDuoType appendType, LogicBlock logic)
        {
            if (m_current == null)
            {
                m_current = logic;
                return this;
            }

            LogicBlock left, right;
            if (sideLeft)
            {
                left = logic;
                right = m_current;
            }
            else
            {
                right = logic;
                left = m_current;
            }
            switch (appendType)
            {
                case AppendDuoType.And:
                    m_current = new AndDuoLogic(left, right);
                    break;
                case AppendDuoType.or:
                    m_current = new OrDuoLogic(left, right);
                    break;
                case AppendDuoType.Xor:
                    m_current = new XorDuoLogic(left, right);
                    break;
                case AppendDuoType.Eqv:
                    m_current = new EquivalentDuoLogic(left, right);
                    break;
                case AppendDuoType.Less:
                    m_current = new LessDuoLogic(left, right);
                    break;
                case AppendDuoType.More:
                    m_current = new MoreDuoLogic(left, right);
                    break;
                case AppendDuoType.LessEq:
                    m_current = new LessOrEqualDuoLogic(left, right);
                    break;
                case AppendDuoType.MoreEq:
                    m_current = new MoreOrEqualDuoLogic(left, right);
                    break;
                default:
                    break;
            }

            return this;
        }

        public LogicBlockBuilder AppendRight(AppendDuoType appendType, LogicBlock logic)
        {
            return Append(false, appendType, logic);
        }
        public LogicBlockBuilder AppendLeft(AppendDuoType appendType, LogicBlock logic)
        {
            return Append(true, appendType, logic);
        }

        public LogicBlockBuilder AppendRight(AppendGroupType appendType, LogicBlock logic)
        {
            return Append(false, appendType, logic);
        }
        public LogicBlockBuilder AppendLeft(AppendGroupType appendType, LogicBlock logic)
        {
            return Append(true, appendType, logic);
        }


        public LogicBlockBuilder AppendLeft(string appendTypeAsChar, params LogicBlock[] logic)
        {
            bool c;
            AppendGroupType t;
            Convert("" + appendTypeAsChar, out c, out t);
            if (c)
                return Append(true, t, logic);
            return this;
        }
        public LogicBlockBuilder AppendLeft(char appendTypeAsChar, LogicBlock logic)
        {
            bool c;
            AppendDuoType t;
            Convert("" + appendTypeAsChar, out c, out t);
            if (c)
                return Append(true, t, logic);
            return this;

        }
        public LogicBlockBuilder AppendRight(string appendTypeAsChar, params LogicBlock[] logic)
        {
            bool c;
            AppendGroupType t;
            Convert("" + appendTypeAsChar, out c, out t);
            if (c)
                return Append(false, t, logic);
            return this;
        }
        public LogicBlockBuilder AppendRight(char appendTypeAsChar, LogicBlock logic)
        {
            bool c;
            AppendDuoType t;
            Convert("" + appendTypeAsChar, out c, out t);
            if (c)
                return Append(false, t, logic);
            return this;

        }

        public void Convert(string c, out bool converted, out AppendDuoType appendType)
        {
            appendType = AppendDuoType.And;
            converted = false;
            switch (c)
            {
                case "+": appendType = AppendDuoType.And; converted = true; break;
                case "&": appendType = AppendDuoType.And; converted = true; break;
                case "≡": appendType = AppendDuoType.Eqv; converted = true; break;
                case "|": appendType = AppendDuoType.or; converted = true; break;
                case "⊗": appendType = AppendDuoType.Xor; converted = true; break;
                case "<": appendType = AppendDuoType.Less; converted = true; break;
                case "<=": appendType = AppendDuoType.LessEq; converted = true; break;
                case "≤": appendType = AppendDuoType.LessEq; converted = true; break;
                case ">": appendType = AppendDuoType.More; converted = true; break;
                case ">=": appendType = AppendDuoType.MoreEq; converted = true; break;
                default:
                    break;
            }
        }
        public void Convert(string c, out bool converted, out AppendGroupType appendType)
        {
            appendType = AppendGroupType.And;
            converted = false;
            switch (c)
            {
                case "+": appendType = AppendGroupType.And; converted = true; break;
                case "&": appendType = AppendGroupType.And; converted = true; break;
                case "≡": appendType = AppendGroupType.Eqv; converted = true; break;
                case "|": appendType = AppendGroupType.or; converted = true; break;
                case "⊗": appendType = AppendGroupType.Xor; converted = true; break;
                default:
                    break;
            }
        }


        public LogicBlockBuilder RedefinedAs(LogicBlock logic)
        {
            m_current = logic;
            return this;
        }

        public LogicBlockBuilder InverseWarp(bool usePriorityWarp = true)
        {
            if (usePriorityWarp)
                m_current = new PriorityLogicBlock(m_current);
            m_current = new InverseLogic(m_current);
            return this;
        }

        public LogicBlockBuilder PriorityWarp()
        {
            m_current = new PriorityLogicBlock(m_current);
            return this;
        }

        public LogicBlockBuilder Start(LogicBlock logic)
        {
            return new LogicBlockBuilder(logic);
        }
        public LogicBlockBuilder Start(bool logic)
        {
            return Start(new PrimitiveBoolLogicBlock(logic));
        }
        public LogicBlockBuilder Start(ComputedBoolean logic)
        {
            return Start(new DeleguateBoolLogicBlock(logic));
        }

        public LogicBlock GetCurrent() { return m_current; }

    }
    public enum AppendSide { Left, Right }
    public enum AppendDuoType { And, or, Xor, Eqv, Less, More, LessEq, MoreEq }
    public enum AppendGroupType { And, or, Xor, Eqv }
}
