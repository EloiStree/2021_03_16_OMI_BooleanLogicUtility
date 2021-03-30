using BooleanRegisterUtilityAPI;
using BooleanRegisterUtilityAPI.BoolParsingToken;
using BooleanRegisterUtilityAPI.BoolParsingToken.GroupOperation;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item;
using BooleanRegisterUtilityAPI.BoolParsingToken.Item.Builder;
using BooleanRegisterUtilityAPI.BoolParsingToken.LogicBlock;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BooleanRegisterUtilityUnitTDD
{
    internal class BLElementToLogicBuilder
    {
        private BL_BuilderElements elements;

        public TokenType[] startType = new TokenType[] {
            TokenType.S_AND,
            TokenType.S_OR,
            TokenType.S_XEQ,
            TokenType.S_XOR,
            TokenType.S_BRACKET,
            TokenType.S_SQUAREBRACKET
        }; 
        public TokenType[] binaryType = new TokenType[] {
            TokenType.B_AND,
            TokenType.B_OR,
            TokenType.B_XOR,
            TokenType.B_ALESSB,
            TokenType.B_ALESSOREQUALB,
            TokenType.B_AMOREB,
            TokenType.B_AMOREOREQUALB,
            TokenType.B_EQU
        };
        public BLElementToLogicBuilder(BL_BuilderElements elements, out   LogicBlock block, out  List<LogicBlock> created, bool useDebug)
        {
            SetDebug(useDebug);
            this.elements = elements;


            List< StringTokenTypeAndSource> tokens = elements.GetTokens().ToList();
            IGroupOfToken group = new GroupOfToken(tokens);

            if(m_useDebug)Console.WriteLine(">>|\n" + group+"\n\n");
            // GROUP THE  ( )
            int antiLoop = 0;
            bool foundOne=true;
            do {
                foundOne = FuseBracket(ref group, TokenType.S_BRACKET, TokenType.E_BRACKET);
                antiLoop++;

            } while (foundOne && antiLoop < 200);
            // GROUP THE  [  ]
           

                do
                {
                    foundOne = false;
                    List<IGroupOfToken> underGroup;
                    group.GetAllUnderGroupList(out underGroup);
                    underGroup = underGroup.Where(k => k != null && k is GroupOfToken).ToList();
                    for (int i = 0; i < underGroup.Count; i++)
                    {
                        IGroupOfToken r = underGroup[i];
                        if (r != null) {
                            if (FuseBracket(ref r, startType, TokenType.E_SQUAREBRACKET)) {

                                foundOne = true;
                            }
                        }
                    }

                      antiLoop++;

                } while (foundOne && antiLoop < 200);



            // GROUP THE  ¬value ¬() ¬[]
            do
            {
                foundOne = false;
                List<IGroupOfToken> underGroup;
                group.GetAllUnderGroupList(out underGroup);
                underGroup = underGroup.Where(k => k != null && k is GroupOfToken).ToList();
                for (int i = 0; i < underGroup.Count; i++)
                {
                    IGroupOfToken r = underGroup[i];
                    if (r != null)
                    {
                        if (FuseInverse(ref r))
                        {

                            foundOne = true;
                        }
                    }
                }
            
                antiLoop++;
            } while (foundOne && antiLoop < 200);


            //do
            //{
            //    foundOne = false;

            //    List<IGroupOfToken> underGroup;
            //    group.GetAllUnderGroupList(out underGroup);
            //    underGroup = underGroup.Where(k => k != null && k is GroupOfToken).ToList();
            //    for (int i = 0; i < underGroup.Count; i++)
            //    {
            //        IGroupOfToken r = underGroup[i];
            //        if (r != null)
            //            if (FuseBinaryGroup(ref r))
            //            {

            //                foundOne = true;
            //            }
            //    }
            //    antiLoop++;
            //} while (foundOne && antiLoop < 200);

            do
            {
                foundOne = false;

                List<IGroupOfToken> underGroup;
                group.GetAllUnderGroupList(out underGroup);
                underGroup = underGroup.Where(k => k != null && k is GroupOfToken).ToList();
                for (int i = 0; i < underGroup.Count; i++)
                {
                    IGroupOfToken r = underGroup[i];
                    if (r != null)
                        if (FuseBinary(ref r))
                        {

                            foundOne = true;
                        }
                }
                antiLoop++;
            } while (foundOne && antiLoop < 200);

            //do
            //{
            //    foundOne = false;

            //    List<IGroupOfToken> underGroup;
            //    group.GetAllUnderGroupList(out underGroup);
            //    underGroup = underGroup.Where(k => k != null && k is GroupOfToken).ToList();
            //    for (int i = 0; i < underGroup.Count; i++)
            //    {
            //        IGroupOfToken r = underGroup[i];
            //        if (r != null)
            //            if (RemoveEmptyUnder(ref r))
            //            {

            //                foundOne = true;
            //            }
            //    }
            //    antiLoop++;
            //} while (foundOne && antiLoop < 200);


            DisplayByLevel(group, 0);
            //TryToConvertToBlLogic(group);


            created= new List<LogicBlock>();
            TryToConvert(group, out block, ref created,0);



        }
        public bool m_useDebug;
        public void SetDebug(bool value)
        {
            m_useDebug = value;
        }



        //private bool RemoveEmptyUnder(ref IGroupOfToken r)
        //{
        //    if (r == null) return false;
        //    GroupOfToken g = r as GroupOfToken;
        //    if (g == null) return false;
        //    List<IGroupOfToken> inner = g.GetInnerGroup();

        //    GroupOfToken t = null;
        //    for (int i = 0; i < inner.Count; i++)
        //    {
        //        t = inner[i] as GroupOfToken;
        //        if (t!=null) {
        //            IGroupOfToken underTarget;
        //            if (t.IsEmptyRef(out underTarget)) { 
        //             //TO CODE
        //            }
        //        }

        //    }

        //}

        //private void TryToConvertToBlLogic(IGroupOfToken group)
        //{
        //    if(m_useDebug)Console.WriteLine("======================");
        //    //            List<StringTokenTypeAndSource> tokens = group.GetAllTokens();
        //    List<IGroupOfToken> innerGroup = group.GetInnerGroup();
        //    List<IGroupOfToken> waitThemToBeConvert = new List<IGroupOfToken>();
        //    for (int i = 0; i < innerGroup.Count; i++)
        //    {
        //        if (innerGroup[i] is LeafToken)
        //        {
        //            LeafToken l = (LeafToken)innerGroup[i];
        //            if(m_useDebug)Console.WriteLine("L:>" + l.GetDescription());

        //        }
        //        else
        //        {
        //            GroupOfToken l = (GroupOfToken)innerGroup[i];
        //            if(m_useDebug)Console.WriteLine("G:>" + l.GetInnerCount());
        //            waitThemToBeConvert.Add(l);

        //        }
        //    }

        //    if(m_useDebug)Console.WriteLine("======================");
        //    for (int i = 0; i < waitThemToBeConvert.Count; i++)
        //    {
        //        TryToConvertToBlLogic(waitThemToBeConvert[i]);
        //    }

        //}
        private void TryToConvert(IGroupOfToken group, out LogicBlock result, ref List<LogicBlock> created, int level)
        {
            level++;
            string levelTab = "";
            for (int i = 0; i < level; i++)
            {
                levelTab += "\t";
            }
            if(m_useDebug)Console.WriteLine(levelTab+"======================");
            result = null;
          
                if (group == null)
                {


                    if(m_useDebug)Console.WriteLine(levelTab + "NULL:> o_O");
                    return;
                }
           


            List<IGroupOfToken> innerGroup = group.GetInnerGroup();
            if (innerGroup != null && innerGroup.Count>0)
            {
                for (int i = 0; i < innerGroup.Count; i++)
                {
                    if (innerGroup[i] is LeafToken)
                    {
                        LeafToken l = (LeafToken)innerGroup[i];
                        if(m_useDebug)Console.WriteLine(levelTab + "L:>" + l.GetDescription());


                    }
                    else
                    {
                        GroupOfToken l = (GroupOfToken)innerGroup[i];
                        if(m_useDebug)Console.WriteLine(levelTab + "G:>" + l.GetInnerCount());

                        if (l.IsSingleAndEqualTo(TokenType.B_EQU))
                        {
                            if(m_useDebug)Console.WriteLine(levelTab + "TEST32");
                        }

                    }
                }

                //ALL ARE CONVERTED
                int innerCount = innerGroup.Count;
                if (innerCount == 3)
                {

                    TokenType start,end;
                    if (innerGroup[0].IsSingle(out start) && innerGroup[2].IsSingle(out end) &&
                        start == TokenType.S_BRACKET && end == TokenType.E_BRACKET) {

                        TryToConvert(innerGroup[1], out result, ref created, level);
                    }


                    TokenType tokenType;
                    if (innerGroup[1].IsSingle(out tokenType) && (
                        tokenType == TokenType.B_ALESSB ||
                        tokenType == TokenType.B_ALESSOREQUALB ||
                        tokenType == TokenType.B_AMOREB ||
                        tokenType == TokenType.B_AMOREOREQUALB ||
                        tokenType == TokenType.B_AND ||
                        tokenType == TokenType.B_EQU ||
                        tokenType == TokenType.B_OR ||
                        tokenType == TokenType.B_XOR)

                        )
                    {

                        LogicBlock left, right;
                        TryToConvert(innerGroup[0], out left, ref created,level);
                        TryToConvert(innerGroup[2], out right, ref created, level);
                        if (tokenType == TokenType.B_AND)
                        {
                            result = new AndDuoLogic(left, right);
                        }
                        if (tokenType == (TokenType.B_OR))
                        {
                            result = new OrDuoLogic(left, right);
                        }
                        if (tokenType == (TokenType.B_ALESSB))
                        {
                            result = new LessDuoLogic(left, right);
                        }
                        if (tokenType == (TokenType.B_ALESSOREQUALB))
                        {
                            result = new LessOrEqualDuoLogic(left, right);
                        }
                        if (tokenType == (TokenType.B_AMOREB))
                        {
                            result = new MoreDuoLogic(left, right);
                        }
                        if (tokenType == (TokenType.B_AMOREOREQUALB))
                        {
                            result = new MoreOrEqualDuoLogic(left, right);
                        }
                        if (tokenType == (TokenType.B_AND))
                        {
                            result = new AndDuoLogic(left, right);
                        }
                        if (tokenType == (TokenType.B_EQU))
                        {
                            result = new EquivalentDuoLogic(left, right);
                        }
                        if (tokenType == (TokenType.B_OR))
                        {
                            result = new OrDuoLogic(left, right);
                        }
                        if (tokenType == (TokenType.B_XOR))
                        {
                            result = new XorDuoLogic(left, right);
                        }


                    }


                    //LogicBlock lb;
                    //TryToConvert(innerGroup[i], out lb, ref created);
                }
                else if (innerCount == 2)
                {

                    if (innerGroup[0].IsSingleAndEqualTo(TokenType.NEGATIVE))
                    {
                        LogicBlock rest;
                        TryToConvert(innerGroup[1], out rest, ref created, level);
                        result = new InverseLogic(rest);
                        if(m_useDebug)Console.WriteLine(levelTab + "NNEGG:>");
                    }

                }
                else if (innerCount == 1)
                {
                    //... ???
                    TryToConvert(innerGroup[0], out result, ref created, level);
                    if(m_useDebug)Console.WriteLine(levelTab + "S:???>");


                }

                else if(innerCount>3)
                {
                    TokenType start, end;
                    if (innerGroup[0].IsSingle(out start) && innerGroup[innerGroup.Count - 1].IsSingle(out end))
                    {
                        if (end == TokenType.E_SQUAREBRACKET) {

                            if (start == TokenType.S_AND || start == TokenType.S_OR || start == TokenType.S_XEQ || start == TokenType.S_XOR) {

                                List<LogicBlock> blist= new List<LogicBlock>();
                                for (int i = 1; i < innerGroup.Count - 1; i++)
                                {
                                    LogicBlock tmp;
                                    TryToConvert(innerGroup[i], out tmp, ref created, level);
                                    blist.Add(tmp);
                                }

                                if (start == TokenType.S_AND)
                                {
                                    result = new AndLogic(blist.ToArray());
                                }
                                else if (start == TokenType.S_OR)
                                {
                                    result = new OrLogic(blist.ToArray());

                                }
                                else if (start == TokenType.S_XEQ)
                                {
                                    result = new EquivalentLogic(blist.ToArray());
                                }
                                else if (start == TokenType.S_XOR)
                                {
                                    result = new XorLogic(blist.ToArray());

                                }


                            }
                            else
                            // Try To convert to domino or go for and by default
                            {
                                bool isDominoLeftTrue= false;
                                bool isDominoRightTrue = false;
                                bool isDominoFound = false;
                                List<LogicBlock> left = new List<LogicBlock>();
                                List<LogicBlock> right = new List<LogicBlock>();
                                for (int i = 1; i < innerGroup.Count - 1; i++)
                                {
                                    if (innerGroup[i].IsSingleAndEqualTo(TokenType.DominoLeftTrue)) { 
                                        isDominoLeftTrue = true; isDominoFound = true;
                                    }
                                    else if (innerGroup[i].IsSingleAndEqualTo(TokenType.DominoRightTrue))
                                    {
                                        isDominoRightTrue = true; isDominoFound = true;
                                    }
                                    else {
                                        LogicBlock tmp;
                                        TryToConvert(innerGroup[i], out tmp, ref created, level);
                                        if (isDominoFound)
                                            right.Add(tmp);
                                        else left.Add(tmp);
                                    }

                                }

                                if (isDominoLeftTrue)
                                {
                                    result = new DominoLogic(left.ToArray(), right.ToArray());
                                }
                                else if (isDominoRightTrue)
                                {
                                    result = new DominoLogic(right.ToArray(), left.ToArray());
                                }
                                else {

                                    result = new AndLogic(left.ToArray());
                                }


                            }
                        }
                        
                        


                    }
                }

            }
            else if (innerGroup==null)
            {

                List<StringTokenTypeAndSource> tokenGroup = group.GetAllTokens();
                if(m_useDebug)Console.WriteLine("--->TC:"+tokenGroup.Count);
                if (tokenGroup.Count == 1)
                {
                    TokenType firstToken = tokenGroup[0].GetTokenType();
                    if(m_useDebug)Console.WriteLine("--->TC,type:" +firstToken);
                    if (firstToken == TokenType.ZERO)
                    {
                        if(m_useDebug)Console.WriteLine("--->0<");
                        result = new LogicBlockZeroOrOne(false);
                    }
                    else if (firstToken == TokenType.ONE)
                    {
                        if(m_useDebug)Console.WriteLine("--->1<");
                        result = new LogicBlockZeroOrOne(true);
                    }
                    else if (firstToken == TokenType.BooleanToken)
                    {
                        if(m_useDebug)Console.WriteLine("--->I<");
                        result = GetBooleanToken(tokenGroup[0]);
                    }

                }
                else {
                    


                    for (int i = 0; i < tokenGroup.Count; i++)
                    {

                        if(m_useDebug)Console.WriteLine(levelTab + "T?:>" + tokenGroup[i]);
                    }
                }
            }

            if (result != null)
            {
                if(m_useDebug)Console.WriteLine(levelTab + "BC:>" + result);
                created.Add(result);
            }
            if(m_useDebug)Console.WriteLine(levelTab + "======");
        }

        private LogicBlock GetBooleanToken(StringTokenTypeAndSource token)
        {
            BL_BooleanItem bl;
            elements.GetBoolItemFrom(token, out bl );
            BL_ToBeDefined tobe = new BL_ToBeDefined(bl);
            return tobe;
        }

        private bool  FuseBinary(ref IGroupOfToken group)
        {
            List<IGroupOfToken> innerGroup = group.GetInnerGroup();

            if (innerGroup == null) return false;
            if (innerGroup.Count <= 3) return false;

            int bracketStart = -1;
            int bracketEnd = -1;



            for (int i = 1; i < innerGroup.Count - 1; i++)
            {

                if (IsLeafAnd(innerGroup[i], binaryType))
                {
                    bool isValideLeft = IsValideForBinaryOperation(innerGroup[i - 1]);
                    bool isValideRight = IsValideForBinaryOperation(innerGroup[i + 1]);

                    if (isValideLeft && isValideRight)

                    {
                        bracketStart = i - 1;
                        bracketEnd = i + 1;

                        break;
                    }
                }
            }

            if (bracketEnd != -1 && bracketStart != -1)
            {
                List<IGroupOfToken> left, center, right;
                Cut(innerGroup, bracketStart, bracketEnd, out left, out center, out right);
                IGroupOfToken g = new GroupOfToken(center);
                left.Add(g);
                left.AddRange(right);
                group.Overwrite(left); 
                return true;
            }
            return false;
        }

        private bool IsValideForBinaryOperation(IGroupOfToken g)
        {
            return IsLeafAnd(g, TokenType.ONE)
                                    || IsLeafAnd(g, TokenType.ZERO)
                                     || IsLeafAnd(g, TokenType.BooleanToken)
                                     || StartWith(g, TokenType.NEGATIVE)
                                     || !IsLeafAnd(g);
        }

        //private bool FuseBinaryGroup(ref IGroupOfToken group,TokenType spliter = TokenType.B_AND)
        //{

        //    //List<int> tokenFound = new List<int>();
        //    //List<IGroupOfToken> innerGroup = group.GetInnerGroup();

        //    //if (innerGroup == null) return false;
        //    //if (innerGroup.Count <= 3) return false;

        //    //int bracketStart = -1;
        //    //int bracketEnd = -1;



        //    //for (int i = 1; i < innerGroup.Count - 1; i++)
        //    //{

        //    //    if (IsLeafAnd(innerGroup[i], spliter)) {
        //    //        tokenFound.Add(i);
        //    //    }

               
        //    //}

        //    ////if (IsLeafAnd(innerGroup[i], binaryType))
        //    ////{
        //    ////    bool isValideLeft = IsValideForBinaryOperation(innerGroup[i - 1]);
        //    ////    bool isValideRight = IsValideForBinaryOperation(innerGroup[i + 1]);

        //    ////    if (isValideLeft && isValideRight)

        //    ////    {
        //    ////        bracketStart = i - 1;
        //    ////        bracketEnd = i + 1;

        //    ////        break;
        //    ////    }
        //    ////}

        //    //if (bracketEnd != -1 && bracketStart != -1)
        //    //{
        //    //    List<IGroupOfToken> left, center, right;
        //    //    Cut(innerGroup, bracketStart, bracketEnd, out left, out center, out right);
        //    //    IGroupOfToken g = new GroupOfToken(center);
        //    //    left.Add(g);
        //    //    left.AddRange(right);
        //    //    group.Overwrite(left);
        //    //    return true;
        //    //}
        //    //return false;

        //}

        private bool StartWith(IGroupOfToken groupOfToken, TokenType lookForToken)
        {
            if (groupOfToken == null) return false;
            List<IGroupOfToken> innerGroup = groupOfToken.GetInnerGroup();
            if (innerGroup == null) return false;
            if (innerGroup.Count > 0) {
                return innerGroup[0].IsSingleAndEqualTo(lookForToken);
            }
            return  false;
        }

        private bool FuseInverse(ref IGroupOfToken group)
        {
            List<IGroupOfToken> innerGroup = group.GetInnerGroup();

            if (innerGroup == null)
                return false;
            if (innerGroup.Count <= 2)
                return false;

            int bracketStart = -1;
            int bracketEnd = -1;
            for (int i = 0; i < innerGroup.Count-1; i++)
            {
                if (IsLeafAnd(innerGroup[i], TokenType.NEGATIVE)) {

                    
                    if (IsLeafAnd(innerGroup[i+1], TokenType.ONE)
                        || IsLeafAnd(innerGroup[i + 1], TokenType.ZERO)
                         || IsLeafAnd(innerGroup[i + 1], TokenType.BooleanToken)
                         || ! IsLeafAnd(innerGroup[i + 1])
                         )
                    {
                        bracketStart = i;
                        bracketEnd = i + 1;

                        break;
                    }
                }
            }

            if (bracketEnd != -1 && bracketStart != -1 )
            {
                List<IGroupOfToken> left, center, right;
                Cut(innerGroup, bracketStart, bracketEnd, out left, out center, out right);
                IGroupOfToken g = new GroupOfToken(center);
                left.Add(g);
                left.AddRange(right);
                group.Overwrite(left);
                return true;
            }
            return false;
        }

        private bool FuseBracket(ref IGroupOfToken group, TokenType betweenStart, TokenType betweenStop)
        {
            return FuseBracket(ref group, new TokenType[] { betweenStart }, betweenStop);
        }
            private bool FuseBracket(ref IGroupOfToken group, TokenType []  betweenStart, TokenType betweenStop)
        {
            int bracketStart = -1;
            int bracketEnd = -1;
            List<IGroupOfToken> innerGroup = group.GetInnerGroup();
            if (innerGroup == null)
                return false;
            if (innerGroup.Count <= 2)
                return false;
            for (int i = 0; i < innerGroup.Count; i++)
            {
                for (int j = 0; j < betweenStart.Length; j++)
                {
                    if (IsLeafAnd(innerGroup[i], betweenStart[j]))
                    {
                        bracketStart = i;
                    }

                }
                if (IsLeafAnd(innerGroup[i], betweenStop))
                {
                    bracketEnd = i;
                    break;
                }
            }

            if (bracketEnd != -1 && bracketStart != -1 && !(bracketStart==0 && bracketEnd==innerGroup.Count-1))
            {
                List<IGroupOfToken> left, center, right;
                Cut(innerGroup, bracketStart, bracketEnd, out left, out center, out right);
                IGroupOfToken g = new GroupOfToken(center);
                left.Add(g);
                left.AddRange(right);
                group.Overwrite(left);
                return true;
            }
            return false;
        }

      
        private bool IsBracketStart(IGroupOfToken groupOfToken)
        {
            TokenType lookingFor = TokenType.S_BRACKET;
            return IsLeafAnd(groupOfToken, lookingFor);
        }
        private bool IsBracketEnd(IGroupOfToken groupOfToken)
        {
            TokenType lookingFor = TokenType.E_BRACKET;
            return IsLeafAnd(groupOfToken, lookingFor);
        }


        private bool IsLeafAnd(IGroupOfToken groupOfToken)
        {
            return groupOfToken is LeafToken;
        }
        private static bool IsLeafAnd(IGroupOfToken groupOfToken, params TokenType [] lookingFor)
        {
            for (int i = 0; i < lookingFor.Length; i++)
            {
                if (IsLeafAnd(groupOfToken, lookingFor[i]))
                    return true;

            }
            return false;
        }

        private static bool IsLeafAnd(IGroupOfToken groupOfToken, TokenType lookingFor)
        {
            if (groupOfToken is LeafToken)
            {
                LeafToken t = (LeafToken)groupOfToken;
                StringTokenTypeAndSource target;
                if (t.IsSingle(out target))
                {
                    return target.GetTokenType() == lookingFor;
                }
            }
            return false;
        }

        private void Cut(List<IGroupOfToken> innerGroup, int bracketStart, int bracketEnd, out List<IGroupOfToken> left, out List<IGroupOfToken> center, out List<IGroupOfToken> right)
        {
            left = new List<IGroupOfToken>();
            center = new List<IGroupOfToken>();
            right = new List<IGroupOfToken>();
            for (int i = 0; i < bracketStart; i++)
            {
                left.Add(innerGroup[i]);

            }
            for (int i = bracketStart; i <= bracketEnd; i++)
            {
                center.Add(innerGroup[i]);

            }
            for (int i = bracketEnd+1; i < innerGroup.Count; i++)
            {
                right.Add(innerGroup[i]);

            }

        }

        private void DisplayByLevel(IGroupOfToken group, int level)
        {
            string tab="";
            for (int i = 0; i < level; i++)
            {
                tab += "\t";

            }

            List<IGroupOfToken> innerGroup = group.GetInnerGroup();
            if (innerGroup != null)
            {
                for (int i = 0; i < innerGroup.Count; i++)
                {
                    if(m_useDebug)Console.WriteLine("|" + tab + "G> " );
                    DisplayByLevel(innerGroup[i], level + 1);
                }
            }
            else {
                List<StringTokenTypeAndSource> leafToken = group.GetAllTokens();
                for (int i = 0; i < leafToken.Count; i++)
                {

                    if(m_useDebug)Console.WriteLine("|" + tab + "T> "+ leafToken[i]);
                }
            }



        }
        
    }


}

public class GroupOfToken: IGroupOfToken
{
    List<IGroupOfToken> m_list = new List<IGroupOfToken>();

    public GroupOfToken(List<StringTokenTypeAndSource> tokens)
    {
        for (int i = 0; i < tokens.Count; i++)
        {
            m_list.Add(new LeafToken(tokens[i]));
        }
    }
    public GroupOfToken(List<IGroupOfToken> group)
    {
        m_list = group;
    }





    public List<StringTokenTypeAndSource> GetAllTokens()
    {
        List<StringTokenTypeAndSource> l = new List<StringTokenTypeAndSource>();
        for (int i = 0; i < m_list.Count; i++)
        {
            l.AddRange(m_list[i].GetAllTokens());
        }
        return l;
    }
    public List<IGroupOfToken> GetInnerGroup()
    {
        return m_list;
    }

    public override string ToString()
    {
        return "G:" + string.Join(" : ", m_list.Select(k=>k.ToString()).ToArray()) ;
    }

    public void Overwrite(List<IGroupOfToken> list)
    {
        m_list = list;
    }

    public void GetAllUnderGroupList(out List<IGroupOfToken> gathering) {
        gathering = new List<IGroupOfToken>();
        gathering.Add(this);
        GetAllUnderGroup(ref gathering);
    }
    public void  GetAllUnderGroup(ref List<IGroupOfToken> gathering)
    {
        for (int i = 0; i < m_list.Count; i++)
        {
            gathering.Add(m_list[i]);
            m_list[i].GetAllUnderGroup(ref gathering);

        }
    }

    public uint GetInnerCount() { return (uint)m_list.Count; }

    public bool IsSingleAndEqualTo(TokenType tokentype)
    {
        return false;
    }

    public bool IsSingle(out TokenType tokenType)
    {
        tokenType = TokenType.UNKOWN;
        return false;
    }

    internal bool IsEmptyRef(out IGroupOfToken underTarget)
    {
        throw new NotImplementedException();
    }
}
public class LeafToken : IGroupOfToken
{
    List<StringTokenTypeAndSource> m_list = new List<StringTokenTypeAndSource>();


    public LeafToken(StringTokenTypeAndSource singleElement)
    {
        m_list.Add(singleElement);
    }
    public LeafToken(params StringTokenTypeAndSource [] elements)
    {
        m_list.AddRange(elements);
    }
    public LeafToken(IEnumerable< StringTokenTypeAndSource> elements)
    {
        m_list.AddRange(elements);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public List<StringTokenTypeAndSource> GetAllTokens()
    {
        return m_list;
    }

    public List<IGroupOfToken> GetInnerGroup()
    {
        return null;
    }

    public override string ToString()
    {
        return "L:"+string.Join(" : ", m_list.Select(k=>k.ToString()).ToArray());
    }

    public bool IsSingle(out StringTokenTypeAndSource target)
    {
        if (m_list.Count == 1) { 
            target= m_list[0];
            return true; 
        }
        else {
            target = null;
            return false; 
        }
    }

    public void GetAllUnderGroup(ref List<IGroupOfToken> gathering)
    {
        return ;
    }

    public void GetAllUnderGroupList(out List<IGroupOfToken> gathering)
    {
        gathering = new List<IGroupOfToken>();
        gathering.Add(this);
    }

    public void Overwrite(List<IGroupOfToken> left)
    {
        return ;
    }

    public uint GetInnerCount() { return (uint)m_list.Count; }

    public string GetDescription()
    {
        if (GetInnerCount() == 1)
        { 
            return m_list[0].ToString();
        }
        else {

            return string.Join(":", m_list.Select(k=>k.ToString()).ToArray());
        }
    }

    public bool IsSingleAndEqualTo(TokenType tokentype)
    {
        return m_list.Count == 1 && m_list[0].GetTokenType() == tokentype; 
    }

    public bool IsSingle(out TokenType tokenType)
    {
        if (m_list.Count != 1) {
            tokenType = TokenType.UNKOWN;
            return false;
        }
        tokenType = m_list[0].GetTokenType();
        return true;
    }
}


public interface IGroupOfToken {

    List<StringTokenTypeAndSource> GetAllTokens();
    List<IGroupOfToken> GetInnerGroup();
    void GetAllUnderGroup(ref List<IGroupOfToken> gathering );
    void GetAllUnderGroupList(out List<IGroupOfToken> gathering);
    void Overwrite(List<IGroupOfToken> left);
    uint GetInnerCount();

    bool IsSingleAndEqualTo(TokenType tokentype);
    bool IsSingle(out TokenType tokenType);
}


//public class GroupOfTokenConverter {
//    public IGroupOfToken m_targetGroup;
//    public LogicBlock m_logic;


//    public bool Converted() { return m_logic != null; }

//    public void TryConverted() {

//        List<IGroupOfToken> t= m_targetGroup.GetInnerGroup();

    
//    }

//}