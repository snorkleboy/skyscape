namespace UI
{
    public struct Icon{
        public int a;
    }
    public struct DisplayMessage{
        public string title;
        public string value;
    }
    public struct BasicUIStruct
    {
        public BasicUIStruct(Icon _icon, DisplayMessage dn,DisplayMessage[] imp, DisplayMessage[] det ){
            icon = _icon;
            displayName = dn;
            important = imp;
            details = det;
        }
        public Icon icon;
        public DisplayMessage displayName;
        public DisplayMessage[] important;
        public DisplayMessage[] details;
        
    }


}