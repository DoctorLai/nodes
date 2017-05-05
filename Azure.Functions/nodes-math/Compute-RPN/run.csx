using System.Net;

    // v2017.5.3

    #region Enum

    /// <summary>
    /// ����������
    /// </summary>
    public enum OperandType {
      /// <summary>
      /// ����
      /// </summary>
      FUNC = 1,

      /// <summary>
      /// ����
      /// </summary>
      DATE = 2,

      /// <summary>
      /// ����
      /// </summary>
      NUMBER = 3,

      /// <summary>
      /// ����
      /// </summary>
      BOOLEAN = 4,

      /// <summary>
      /// �ַ���
      /// </summary>
      STRING = 5
    }

    /// <summary>
    /// ���������(���ϵ������ȼ����εݼ�)����ֵԽ�����ȼ�Խ��
    /// </summary>
    public enum OperatorType {
      /// <summary>
      /// ������:(,left bracket
      /// </summary>
      LB = 10,

      /// <summary>
      /// ������),right bracket
      /// </summary>
      RB = 11,

      /// <summary>
      /// �߼���,!,NOT
      /// </summary>
      NOT = 20,

      /// <summary>
      /// ����,+,positive sign
      /// </summary>
      PS = 21,

      /// <summary>
      /// ����,-,negative sign
      /// </summary>
      NS = 22,

      /// <summary>
      /// ����ֵ��abs
      /// </summary>
      ABS = 31,
      /// <summary>
      /// ���ڵ���ָ�����ֵ���С����, ceiling
      /// </summary>
      Ceiling = 32,
      /// <summary>
      /// С�ڵ���ָ�����ֵ��������, floor
      /// </summary>
      Floor = 33,
      /// <summary>
      /// e����
      /// </summary>
      EXP = 34,

      /// <summary>
      /// ���У�tan
      /// </summary>
      TAN = 61,
      /// <summary>
      /// �����У�atan
      /// </summary>
      ATAN = 62,
      /// <summary>
      /// ���ң�sin
      /// </summary>
      SIN = 63,
      /// <summary>
      /// �����ң�asin
      /// </summary>
      ASIN = 64,
      /// <summary>
      /// ˫�����ң�sinh
      /// </summary>
      SINH = 65,
      /// <summary>
      /// ���ң�cos
      /// </summary>
      COS = 66,
      /// <summary>
      /// �����ң�acos
      /// </summary>
      ACOS = 67,
      /// <summary>
      /// ˫�����ң�cosh
      /// </summary>
      COSH = 68,


      /// <summary>
      /// ��,*,multiplication
      /// </summary>
      MUL = 100,

      /// <summary>
      /// ��,/,division
      /// </summary>
      DIV = 101,

      /// <summary>
      /// ��,%,modulus
      /// </summary>
      MOD = 102,

      /// <summary>
      /// ��,+,Addition
      /// </summary>
      ADD = 110,

      /// <summary>
      /// ��,-,subtraction
      /// </summary>
      SUB = 111,

      /// <summary>
      /// С��,less than
      /// </summary>
      LT = 120,

      /// <summary>
      /// С�ڻ����,less than or equal to
      /// </summary>
      LE = 121,

      /// <summary>
      /// ����,>,greater than
      /// </summary>
      GT = 122,

      /// <summary>
      /// ���ڻ����,>=,greater than or equal to
      /// </summary>
      GE = 123,

      /// <summary>
      /// ����,=,equal to
      /// </summary>
      ET = 130,

      /// <summary>
      /// ������,unequal to
      /// </summary>
      UT = 131,

      /// <summary>
      /// �߼���,&,AND
      /// </summary>
      AND = 140,

      /// <summary>
      /// �߼���,|,OR
      /// </summary>
      OR = 141,

      /// <summary>
      /// ����,comma
      /// </summary>
      CA = 150,

      /// <summary>
      /// �������� @
      /// </summary>
      END = 255,

      /// <summary>
      /// �������
      /// </summary>
      ERR = 256

    }

    #endregion Enum


    #region Classes

    public class Operand {
      #region Constructed Function
      public Operand(OperandType type, object value) {
        this.Type = type;
        this.Value = value;
      }

      public Operand(string opd, object value) {
        this.Type = ConvertOperand( opd );
        this.Value = value;
      }
      #endregion

      #region Variable &��Property
      /// <summary>
      /// ����������
      /// </summary>
      public OperandType Type { get; set; }

      /// <summary>
      /// �ؼ���
      /// </summary>
      public string Key { get; set; }

      /// <summary>
      /// ������ֵ
      /// </summary>
      public object Value { get; set; }

      #endregion

      #region Public Method
      /// <summary>
      /// ת����������ָ��������
      /// </summary>
      /// <param name="opd">������</param>
      /// <returns>���ض�Ӧ�Ĳ���������</returns>
      public static OperandType ConvertOperand(string opd) {
        if (opd.IndexOf( "(" ) > -1) {
          return OperandType.FUNC;
        }
        else if (IsNumber( opd )) {
          return OperandType.NUMBER;
        }
        else if (IsDate( opd )) {
          return OperandType.DATE;
        }
        else {
          return OperandType.STRING;
        }
      }

      /// <summary>
      /// �ж϶����Ƿ�Ϊ����
      /// </summary>
      /// <param name="value">����ֵ</param>
      /// <returns>�Ƿ�����,�񷵻ؼ�</returns>
      public static bool IsNumber(object value) {
        double val;
        return double.TryParse( value.ToString(), out val );
      }

      /// <summary>
      /// �ж϶����Ƿ�Ϊ����
      /// </summary>
      /// <param name="value">����ֵ</param>
      /// <returns>�Ƿ�����,�񷵻ؼ�</returns>
      public static bool IsDate(object value) {
        DateTime dt;
        return DateTime.TryParse( value.ToString(), out dt );
      }
      #endregion
    }

    public class Operator {
      public Operator(OperatorType type, string value) {
        this.Type = type;
        this.Value = value;
      }

      /// <summary>
      /// ���������
      /// </summary>
      public OperatorType Type { get; set; }

      /// <summary>
      /// �����ֵ
      /// </summary>
      public string Value { get; set; }


      /// <summary>
      /// ����>����&lt;��������ж�ʵ���Ƿ�Ϊ>=,&lt;&gt;��&lt;=����������ǰ�����λ��
      /// </summary>
      /// <param name="currentOpt">��ǰ�����</param>
      /// <param name="currentExp">��ǰ���ʽ</param>
      /// <param name="currentOptPos">��ǰ�����λ��</param>
      /// <param name="adjustOptPos">�����������λ��</param>
      /// <returns>���ص�����������</returns>
      public static string AdjustOperator(string currentOpt, string currentExp, ref int currentOptPos) {
        switch (currentOpt) {
          case "<":
            if (currentExp.Substring( currentOptPos, 2 ) == "<=") {
              currentOptPos++;
              return "<=";
            }
            if (currentExp.Substring( currentOptPos, 2 ) == "<>") {
              currentOptPos++;
              return "<>";
            }
            return "<";

          case ">":
            if (currentExp.Substring( currentOptPos, 2 ) == ">=") {
              currentOptPos++;
              return ">=";
            }
            return ">";
          case "a":
            if (currentExp.Substring( currentOptPos, 3 ) == "abs") {
              currentOptPos += 3;
              return "abs";
            }
            else if (currentExp.Substring( currentOptPos, 4 ) == "atan") {
              currentOptPos += 3;
              return "atan";
            }
            else if (currentExp.Substring( currentOptPos, 4 ) == "asin") {
              currentOptPos += 3;
              return "asin";
            }
            else if (currentExp.Substring( currentOptPos, 4 ) == "acos") {
              currentOptPos += 3;
              return "acos";
            }
            return "error";
          case "c":
            if (currentExp.Substring( currentOptPos, 4 ) == "cosh") {
              currentOptPos += 4;
              return "cosh";
            }
            if (currentExp.Substring( currentOptPos, 3 ) == "cos") {
              currentOptPos += 3;
              return "cos";
            }
            
            if (currentExp.Substring( currentOptPos, 7 ) == "ceiling") {
              currentOptPos += 7;
              return "ceiling";
            }
            return "error";
          case "e":
            if (currentExp.Substring( currentOptPos, 3 ) == "exp") {
              currentOptPos += 3;
              return "exp";
            }
            return "error";
          case "f":
            if (currentExp.Substring( currentOptPos, 5 ) == "floor") {
              currentOptPos += 5;
              return "floor";
            }
            return "error";
          case "s":
            if (currentExp.Substring( currentOptPos, 4 ) == "sinh") {
              currentOptPos += 4;
              return "sinh";
            }
            if (currentExp.Substring( currentOptPos, 3 ) == "sin") {
              currentOptPos += 3;
              return "sin";
            }
            
            return "error";
          case "t":
            if (currentExp.Substring( currentOptPos, 3 ) == "tan") {
              currentOptPos += 2;
              return "tan";
            }
            return "error";
          default:
            return currentOpt;
        }
      }

      /// <summary>
      /// ת���������ָ��������
      /// </summary>
      /// <param name="opt">�����</param>
      /// <param name="isBinaryOperator">�Ƿ�Ϊ��Ԫ�����</param>
      /// <returns>����ָ�������������</returns>
      public static OperatorType ConvertOperator(string opt, bool isBinaryOperator) {
        switch (opt) {
          case "!": return OperatorType.NOT;
          case "+": return isBinaryOperator ? OperatorType.ADD : OperatorType.PS;
          case "-": return isBinaryOperator ? OperatorType.SUB : OperatorType.NS;
          case "*": return isBinaryOperator ? OperatorType.MUL : OperatorType.ERR;
          case "/": return isBinaryOperator ? OperatorType.DIV : OperatorType.ERR;
          case "%": return isBinaryOperator ? OperatorType.MOD : OperatorType.ERR;
          case "<": return isBinaryOperator ? OperatorType.LT : OperatorType.ERR;
          case ">": return isBinaryOperator ? OperatorType.GT : OperatorType.ERR;
          case "<=": return isBinaryOperator ? OperatorType.LE : OperatorType.ERR;
          case ">=": return isBinaryOperator ? OperatorType.GE : OperatorType.ERR;
          case "<>": return isBinaryOperator ? OperatorType.UT : OperatorType.ERR;
          case "=": return isBinaryOperator ? OperatorType.ET : OperatorType.ERR;
          case "&": return isBinaryOperator ? OperatorType.AND : OperatorType.ERR;
          case "|": return isBinaryOperator ? OperatorType.OR : OperatorType.ERR;
          case ",": return isBinaryOperator ? OperatorType.CA : OperatorType.ERR;
          case "@": return isBinaryOperator ? OperatorType.END : OperatorType.ERR;
          default: return OperatorType.ERR;
        }
      }

      /// <summary>
      /// ת���������ָ��������
      /// </summary>
      /// <param name="opt">�����</param>
      /// <returns>����ָ�������������</returns>
      public static OperatorType ConvertOperator(string opt) {
        switch (opt) {
          case "!": return OperatorType.NOT;
          case "+": return OperatorType.ADD;
          case "-": return OperatorType.SUB;
          case "*": return OperatorType.MUL;
          case "/": return OperatorType.DIV;
          case "%": return OperatorType.MOD;
          case "<": return OperatorType.LT;
          case ">": return OperatorType.GT;
          case "<=": return OperatorType.LE;
          case ">=": return OperatorType.GE;
          case "<>": return OperatorType.UT;
          case "=": return OperatorType.ET;
          case "&": return OperatorType.AND;
          case "|": return OperatorType.OR;
          case ",": return OperatorType.CA;
          case "@": return OperatorType.END;
          case "abs": return OperatorType.ABS;
          case "ceiling": return OperatorType.Ceiling;
          case "floor": return OperatorType.Floor;
          case "exp": return OperatorType.EXP;
          case "sin": return OperatorType.SIN;
          case "asin": return OperatorType.ASIN;
          case "cos": return OperatorType.COS;
          case "acos": return OperatorType.ACOS;
          case "tan": return OperatorType.TAN;
          case "atan": return OperatorType.ATAN;
          default: return OperatorType.ERR;
        }
      }

      /// <summary>
      /// ������Ƿ�Ϊ��Ԫ�����,�÷��������⣬�ݲ�ʹ��
      /// </summary>
      /// <param name="tokens">�﷨��Ԫ��ջ</param>
      /// <param name="operators">�������ջ</param>
      /// <param name="currentOpd">��ǰ������</param>
      /// <returns>�Ƿ�����,�񷵻ؼ�</returns>
      public static bool IsBinaryOperator(ref Stack<object> tokens, ref Stack<Operator> operators, string currentOpd) {
        if (currentOpd != "") {
          return true;
        }
        else {
          object token = tokens.Peek();
          if (token is Operand) {
            if (operators.Peek().Type != OperatorType.LB) {
              return true;
            }
            else {
              return false;
            }
          }
          else {
            if (( (Operator)token ).Type == OperatorType.RB) {
              return true;
            }
            else {
              return false;
            }
          }
        }
      }

      /// <summary>
      /// ��������ȼ��Ƚ�
      /// </summary>
      /// <param name="optA">���������A</param>
      /// <param name="optB">���������B</param>
      /// <returns>A��B��ȣ�-1���ͣ�0,��ȣ�1����</returns>
      public static int ComparePriority(OperatorType optA, OperatorType optB) {
        if (optA == optB) {
          //A��B���ȼ����
          return 0;
        }

        //��,��,��(*,/,%)
        if (( optA >= OperatorType.MUL && optA <= OperatorType.MOD ) &&
            ( optB >= OperatorType.MUL && optB <= OperatorType.MOD )) {
          return 0;
        }
        //��,��(+,-)
        if (( optA >= OperatorType.ADD && optA <= OperatorType.SUB ) &&
            ( optB >= OperatorType.ADD && optB <= OperatorType.SUB )) {
          return 0;
        }
        //С��,С�ڻ����,����,���ڻ����(<,<=,>,>=)
        if (( optA >= OperatorType.LT && optA <= OperatorType.GE ) &&
            ( optB >= OperatorType.LT && optB <= OperatorType.GE )) {
          return 0;
        }
        //����,������(=,<>)
        if (( optA >= OperatorType.ET && optA <= OperatorType.UT ) &&
            ( optB >= OperatorType.ET && optB <= OperatorType.UT )) {
          return 0;
        }
        //���Ǻ���
        if (( optA >= OperatorType.ABS && optA <= OperatorType.COSH ) &&
                ( optB >= OperatorType.ABS && optB <= OperatorType.COSH )) {
          return 0;
        }

        if (optA < optB) {
          //A���ȼ�����B
          return 1;
        }

        //A���ȼ�����B
        return -1;

      }
    }

    /// <summary>
    /// Reverse Polish Notation
    /// �沨��ʽ
    /// </summary>
    public class RPN {
      Stack<object> m_tokens = new Stack<object>();
      /// <summary>
      /// �����沨��ʽ��ջ
      /// </summary>
      public Stack<object> Tokens {
        get { return m_tokens; }
      }

      private string _RPNExpression;
      /// <summary>
      /// ���ɵ��沨��ʽ�ַ���
      /// </summary>
      public string RPNExpression {
        get {
          if (_RPNExpression == null) {
            foreach (var item in Tokens) {
              if (item is Operand) {
                _RPNExpression += ( (Operand)item ).Value + ",";
              }
              if (item is Operator) {
                _RPNExpression += ( (Operator)item ).Value + ",";
              }
            }
          }
          return _RPNExpression;
        }
      }

      //����ʹ�õ������
      List<string> m_Operators = new List<string>( new string[]{
        "(",")","abs","ceiling","floor","tan","atan","sin","asin", "cos", "acos","sinh","cosh",
        "!","*","/","%","+","-","<",">","=","&","|",",","@"} );


      /// <summary>
      /// �����ʽ���������(˫���š������š����š���������)�Ƿ�ƥ��
      /// </summary>
      /// <param name="exp"></param>
      /// <returns></returns>
      private bool IsMatching(string exp) {
        string opt = "";    //��ʱ�洢 " ' # (

        for (int i = 0; i < exp.Length; i++) {
          string chr = exp.Substring( i, 1 );   //��ȡÿ���ַ�
          if ("\"'#".Contains( chr ))   //��ǰ�ַ���˫���š������š����ŵ�һ��
          {
            if (opt.Contains( chr ))  //֮ǰ�Ѿ����������ַ�
            {
              opt = opt.Remove( opt.IndexOf( chr ), 1 );  //�Ƴ�֮ǰ�����ĸ��ַ�����ƥ����ַ�
            }
            else {
              opt += chr;     //��һ�ζ������ַ�ʱ���洢
            }
          }
          else if ("()".Contains( chr ))    //��������
          {
            if (chr == "(") {
              opt += chr;
            }
            else if (chr == ")") {
              if (opt.Contains( "(" )) {
                opt = opt.Remove( opt.IndexOf( "(" ), 1 );
              }
              else {
                return false;
              }
            }
          }
        }
        return ( opt == "" );
      }

      /// <summary>
      /// �ӱ��ʽ�в��������λ��
      /// </summary>
      /// <param name="exp">���ʽ</param>
      /// <param name="findOpt">Ҫ���ҵ������</param>
      /// <returns>���������λ��</returns>
      private int FindOperator(string exp, string findOpt) {
        string opt = "";
        for (int i = 0; i < exp.Length; i++) {
          string chr = exp.Substring( i, 1 );
          if ("\"'#".Contains( chr ))//����˫���š������š������е������
          {
            if (opt.Contains( chr )) {
              opt = opt.Remove( opt.IndexOf( chr ), 1 );
            }
            else {
              opt += chr;
            }
          }
          if (opt == "") {
            if (findOpt != "") {
              if (findOpt == chr) {
                return i;
              }
            }
            else {
              if (m_Operators.Exists( x => x.Contains( chr ) )) {
                return i;
              }
            }
          }
        }
        return -1;
      }

      /// <summary>
      /// �﷨����,����׺���ʽת���ɺ�׺���ʽ(���沨�����ʽ)
      /// </summary>
      /// <param name="exp"></param>
      /// <returns></returns>
      public bool Parse(string exp) {
        m_tokens.Clear();//����﷨��Ԫ��ջ
        if (exp.Trim() == "")//���ʽ����Ϊ��
        {
          return false;
        }
        else if (!this.IsMatching( exp ))//���š����š������ŵȱ������
        {
          return false;
        }

        Stack<object> operands = new Stack<object>();             //��������ջ
        Stack<Operator> operators = new Stack<Operator>();      //�������ջ
        OperatorType optType = OperatorType.ERR;                //���������
        string curOpd = "";                                 //��ǰ������
        string curOpt = "";                                 //��ǰ�����
        int curPos = 0;                                     //��ǰλ��
                                                            //int funcCount = 0;                                        //��������

        curPos = FindOperator( exp, "" );

        exp += "@"; //����������
        while (true) {
          curPos = FindOperator( exp, "" );

          curOpd = exp.Substring( 0, curPos ).Trim();
          curOpt = exp.Substring( curPos, 1 );

          //////////////���Դ���///////////////////////////////////
          //System.Diagnostics.Debug.WriteLine("***************");
          //System.Diagnostics.Debug.WriteLine("��ǰ��ȡ�Ĳ�������" + curOpd);

          //foreach (var item in operands.ToArray())
          //{
          //    if (item is Operand)
          //    {
          //        System.Diagnostics.Debug.WriteLine("������ջ��" + ((Operand)item).Value);
          //    }
          //    if (item is Operator)
          //    {
          //        System.Diagnostics.Debug.WriteLine("������ջ��" + ((Operator)item).Value);
          //    }
          //}

          //System.Diagnostics.Debug.WriteLine("��ǰ��ȡ���������" + curOpt);
          //foreach (var item in operators.ToArray())
          //{
          //    System.Diagnostics.Debug.WriteLine("�����ջ��" + item.Value);
          //}
          ////////////////////////////////////////////////////////

          //�洢��ǰ����������������ջ
          if (curOpd != "") {
            operands.Push( new Operand( curOpd, curOpd ) );
          }

          //����ǰ�����Ϊ�������������ֹͣѭ��
          if (curOpt == "@") {
            break;
          }
          //����ǰ�����Ϊ������,��ֱ�Ӵ����ջ��
          if (curOpt == "(") {
            operators.Push( new Operator( OperatorType.LB, "(" ) );
            exp = exp.Substring( curPos + 1 ).Trim();
            continue;
          }

          //����ǰ�����Ϊ������,�����ε����������ջ�е�����������뵽��������ջ,ֱ������������Ϊֹ,��ʱ������������.
          if (curOpt == ")") {
            while (operators.Count > 0) {
              if (operators.Peek().Type != OperatorType.LB) {
                operands.Push( operators.Pop() );
              }
              else {
                operators.Pop();
                break;
              }
            }
            exp = exp.Substring( curPos + 1 ).Trim();
            continue;
          }


          //���������
          curOpt = Operator.AdjustOperator( curOpt, exp, ref curPos );

          optType = Operator.ConvertOperator( curOpt );

          //���������ջΪ��,�������������ջջ��Ϊ������,�򽫵�ǰ�����ֱ�Ӵ����������ջ.
          if (operators.Count == 0 || operators.Peek().Type == OperatorType.LB) {
            operators.Push( new Operator( optType, curOpt ) );
            exp = exp.Substring( curPos + 1 ).Trim();
            continue;
          }

          //����ǰ��������ȼ����������ջ���������,�򽫵�ǰ�����ֱ�Ӵ����������ջ.
          if (Operator.ComparePriority( optType, operators.Peek().Type ) > 0) {
            operators.Push( new Operator( optType, curOpt ) );
          }
          else {
            //����ǰ����������������ջջ������������ȼ��ͻ���ȣ������ջ�����������������ջ��ֱ�������ջջ����������ڣ����������ڣ�����������ȼ���
            //�������ջջ�������Ϊ������
            //������ǰ�����ѹ���������ջ��
            while (operators.Count > 0) {
              if (Operator.ComparePriority( optType, operators.Peek().Type ) <= 0 && operators.Peek().Type != OperatorType.LB) {
                operands.Push( operators.Pop() );

                if (operators.Count == 0) {
                  operators.Push( new Operator( optType, curOpt ) );
                  break;
                }
              }
              else {
                operators.Push( new Operator( optType, curOpt ) );
                break;
              }
            }

          }
          exp = exp.Substring( curPos + 1 ).Trim();
        }
        //ת�����,���������ջ�����������ʱ,
        //������ȡ�����������������ջ,ֱ���������ջΪ��
        while (operators.Count > 0) {
          operands.Push( operators.Pop() );
        }
        //����������ջ�ж����˳�����������ջ
        while (operands.Count > 0) {
          m_tokens.Push( operands.Pop() );
        }

        return true;
      }

      /// <summary>
      /// �沨�����ʽ��ֵ�㷨
      /// </summary>
      /// <returns></returns>
      public object Evaluate() {
        /*
          �沨�����ʽ��ֵ�㷨��
          1��ѭ��ɨ���﷨��Ԫ����Ŀ��
          2�����ɨ�����Ŀ�ǲ�����������ѹ���������ջ����ɨ����һ����Ŀ��
          3�����ɨ�����Ŀ��һ����Ԫ����������ջ�Ķ�������������ִ�и����㡣
          4�����ɨ�����Ŀ��һ��һԪ����������ջ����ϲ�����ִ�и����㡣
          5��������������ѹ���ջ��
          6���ظ�����2-5����ջ�м�Ϊ���ֵ��
        */

        if (m_tokens.Count == 0) return null;

        object value = null;
        Stack<Operand> opds = new Stack<Operand>();
        Stack<object> pars = new Stack<object>();
        Operand opdA, opdB;

        foreach (object item in m_tokens) {
          if (item is Operand) {
            //TODO ������ʽ���滻����
            
            //���Ϊ��������ѹ���������ջ
            opds.Push( (Operand)item );
          }
          else {
            switch (( (Operator)item ).Type) {
              #region ��,*,multiplication
              case OperatorType.MUL:
                opdA = opds.Pop();
                opdB = opds.Pop();
                if (Operand.IsNumber( opdA.Value ) && Operand.IsNumber( opdB.Value )) {
                  opds.Push( new Operand( OperandType.NUMBER, double.Parse( opdB.Value.ToString() ) * double.Parse( opdA.Value.ToString() ) ) );
                }
                else {
                  throw new Exception( "����������������������Ϊ����" );
                }
                break;
              #endregion

              #region ��,/,division
              case OperatorType.DIV:
                opdA = opds.Pop();
                opdB = opds.Pop();
                if (Operand.IsNumber( opdA.Value ) && Operand.IsNumber( opdB.Value )) {
                  opds.Push( new Operand( OperandType.NUMBER, double.Parse( opdB.Value.ToString() ) / double.Parse( opdA.Value.ToString() ) ) );
                }
                else {
                  throw new Exception( "����������������������Ϊ����" );
                }
                break;
              #endregion

              #region ��,%,modulus
              case OperatorType.MOD:
                opdA = opds.Pop();
                opdB = opds.Pop();
                if (Operand.IsNumber( opdA.Value ) && Operand.IsNumber( opdB.Value )) {
                  opds.Push( new Operand( OperandType.NUMBER, double.Parse( opdB.Value.ToString() ) % double.Parse( opdA.Value.ToString() ) ) );
                }
                else {
                  throw new Exception( "����������������������Ϊ����" );
                }
                break;
              #endregion

              #region ��,+,Addition
              case OperatorType.ADD:
                opdA = opds.Pop();
                opdB = opds.Pop();
                if (Operand.IsNumber( opdA.Value ) && Operand.IsNumber( opdB.Value )) {
                  opds.Push( new Operand( OperandType.NUMBER, double.Parse( opdB.Value.ToString() ) + double.Parse( opdA.Value.ToString() ) ) );
                }
                else {
                  throw new Exception( "����������������������Ϊ����" );
                }
                break;
              #endregion

              #region ��,-,subtraction
              case OperatorType.SUB:
                opdA = opds.Pop();
                opdB = opds.Pop();
                if (Operand.IsNumber( opdA.Value ) && Operand.IsNumber( opdB.Value )) {
                  opds.Push( new Operand( OperandType.NUMBER, double.Parse( opdB.Value.ToString() ) - double.Parse( opdA.Value.ToString() ) ) );
                }
                else {
                  throw new Exception( "����������������������Ϊ����" );
                }
                break;
              #endregion

              #region ����ֵ,abs,absolute
              case OperatorType.ABS:
                opdA = opds.Pop();
                if (Operand.IsNumber( opdA.Value )) {
                  opds.Push( new Operand( OperandType.NUMBER, Math.Abs( double.Parse( opdA.Value.ToString() ) ) ) );
                }
                else {
                  throw new Exception( "����ֵ�����1�������������Ϊ����" );
                }
                break;
              #endregion

              #region ���ڵ���ָ�����ֵ���С����, ceiling
              case OperatorType.Ceiling:
                opdA = opds.Pop();
                if (Operand.IsNumber( opdA.Value )) {
                  opds.Push( new Operand( OperandType.NUMBER, Math.Ceiling( double.Parse( opdA.Value.ToString() ) ) ) );
                }
                else {
                  throw new Exception( "����ֵ�����1�������������Ϊ����" );
                }
                break;
              #endregion

              #region С�ڵ���ָ�����ֵ��������, ceiling
              case OperatorType.Floor:
                opdA = opds.Pop();
                if (Operand.IsNumber( opdA.Value )) {
                  opds.Push( new Operand( OperandType.NUMBER, Math.Floor( double.Parse( opdA.Value.ToString() ) ) ) );
                }
                else {
                  throw new Exception( "����ֵ�����1�������������Ϊ����" );
                }
                break;
              #endregion

              #region e��ָ������,exp
              case OperatorType.EXP:
                opdA = opds.Pop();
                if (Operand.IsNumber( opdA.Value )) {
                  opds.Push( new Operand( OperandType.NUMBER, Math.Exp( double.Parse( opdA.Value.ToString() ) ) ) );
                }
                else {
                  throw new Exception( "e��ָ�����������1�������������Ϊ����" );
                }
                break;
              #endregion

              #region ����,tan,subtraction
              case OperatorType.TAN:
                opdA = opds.Pop();
                if (Operand.IsNumber( opdA.Value )) {
                  opds.Push( new Operand( OperandType.NUMBER, Math.Tan( double.Parse( opdA.Value.ToString() ) * Math.PI / 180 ) ) );
                }
                else {
                  throw new Exception( "���������1�������������Ϊ�Ƕ�����" );
                }
                break;
              #endregion

              #region ������,atan,subtraction
              case OperatorType.ATAN:
                opdA = opds.Pop();
                if (Operand.IsNumber( opdA.Value )) {
                  opds.Push( new Operand( OperandType.NUMBER, Math.Atan( double.Parse( opdA.Value.ToString() ) ) ) );
                }
                else {
                  throw new Exception( "�����������1�������������Ϊ����" );
                }
                break;
              #endregion

              #region ����,sin,sine
              case OperatorType.SIN:
                opdA = opds.Pop();
                if (Operand.IsNumber( opdA.Value )) {
                  opds.Push( new Operand( OperandType.NUMBER, Math.Sin( double.Parse( opdA.Value.ToString() ) * Math.PI / 180 ) ) );
                }
                else {
                  throw new Exception( "���������1�������������Ϊ�Ƕ�����" );
                }
                break;
              #endregion

              #region ������,asin,arcsine
              case OperatorType.ASIN:
                opdA = opds.Pop();
                if (Operand.IsNumber( opdA.Value )) {
                  opds.Push( new Operand( OperandType.NUMBER, Math.Asin( double.Parse( opdA.Value.ToString() ) ) ) );
                }
                else {
                  throw new Exception( "�����������1�������������Ϊ����" );
                }
                break;
              #endregion

              #region ˫������,sinh,hyperbolic sine
              case OperatorType.SINH:
                opdA = opds.Pop();
                if (Operand.IsNumber( opdA.Value )) {
                  opds.Push( new Operand( OperandType.NUMBER, Math.Sinh( double.Parse( opdA.Value.ToString() ) * Math.PI / 180 ) ) );
                }
                else {
                  throw new Exception( "˫�����������1�������������Ϊ�Ƕ�����" );
                }
                break;
              #endregion

              #region ����,cos,cosine
              case OperatorType.COS:
                opdA = opds.Pop();
                if (Operand.IsNumber( opdA.Value )) {
                  opds.Push( new Operand( OperandType.NUMBER, Math.Cos( double.Parse( opdA.Value.ToString() ) * Math.PI / 180 ) ) );
                }
                else {
                  throw new Exception( "���������1�������������Ϊ�Ƕ�����" );
                }
                break;
              #endregion

              #region ������,acos,arc cosine
              case OperatorType.ACOS:
                opdA = opds.Pop();
                if (Operand.IsNumber( opdA.Value )) {
                  opds.Push( new Operand( OperandType.NUMBER, Math.Acos( double.Parse( opdA.Value.ToString() ) ) ) );
                }
                else {
                  throw new Exception( "�����������1�������������Ϊ����" );
                }
                break;
              #endregion

              #region ˫������,cosh,hyperbolic cosine
              case OperatorType.COSH:
                opdA = opds.Pop();
                if (Operand.IsNumber( opdA.Value )) {
                  opds.Push( new Operand( OperandType.NUMBER, Math.Cosh( double.Parse( opdA.Value.ToString() ) * Math.PI / 180 ) ) );
                }
                else {
                  throw new Exception( "˫�����������1�������������Ϊ�Ƕ�����" );
                }
                break;
                #endregion

            }
          }
        }

        if (opds.Count == 1) {
          value = opds.Pop().Value;
        }

        return value;
      }

    }

    #endregion Classes

    public const string Param1Key_Expression = "expression";

    public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log) {
      log.Info( "Math: Compute-RPN(Reverse Polish Notation)" );
      string result = null;
      string exp = req.GetQueryNameValuePairs()
          .FirstOrDefault( q => string.Compare( q.Key, Param1Key_Expression, true ) == 0 )
          .Value;
      //if (exp == null && req.Properties.ContainsKey( Param1Key_Expression ))
      //  exp = req.Properties[Param1Key_Expression] as string;
      //else if (exp == null && req.Headers.Contains( Param1Key_Expression ))
      //  exp = req.Headers.GetValues( Param1Key_Expression ).FirstOrDefault();

      if (string.IsNullOrWhiteSpace( exp )) {
        return req.CreateResponse( HttpStatusCode.BadRequest, "need a parameter named Expression" );
      }
      log.Info( "RPN(Reverse Polish Notation) expression: " + exp );

      RPN rpn = new RPN();
      if (rpn.Parse( exp )) {
        var r = rpn.Evaluate();
        if (r == null) {
          return req.CreateResponse( HttpStatusCode.BadRequest, "some error occured in RPN.Evaluate()" );
        }
        result = r.ToString();
      }
      else {
        return req.CreateResponse( HttpStatusCode.BadRequest, "RPN.Parse method failed" );
      }

      return result == null ?
      req.CreateResponse( HttpStatusCode.BadRequest, "unknown error" )
      : req.CreateResponse( HttpStatusCode.OK, result, "text/plain" );
    }
