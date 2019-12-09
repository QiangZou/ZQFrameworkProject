

public static class ExtensionsUIScrollView 
{
    /// <summary>
    /// 设置位置
    /// </summary>
    /// <param name="self"></param>
    /// <param name="total">总个数</param>
    /// <param name="target">目标</param>
    public static void SetPosition(this UIScrollView self, int total, int target)
    {
        UIGrid uiGrid = self.transform.GetComponentInChildren<UIGrid>(true);
        if (uiGrid == null)
        {
            return;
        }

        target -= 1;

        float value = 0;

        if (self.movement == UIScrollView.Movement.Vertical)
        {
            //滑动对象总高长度
            float objectTotalLength = uiGrid.cellHeight * total;
            //Panel总高长度
            float panelTotalLength = self.panel.GetViewSize().y;

            //计算占比
            value = (target * uiGrid.cellHeight) / (objectTotalLength - panelTotalLength);
        }
        else if (self.movement == UIScrollView.Movement.Horizontal)
        {
            //滑动对象总宽长度
            float objectTotalLength = uiGrid.cellWidth * total;
            //Panel总宽长度
            float panelTotalLength = self.panel.GetViewSize().x;

            //计算占比
            value = (target * uiGrid.cellWidth) / (objectTotalLength - panelTotalLength);
        }

        //容错
        value = value >= 1 ? 1 : value;
        value = value <= 0 ? 0 : value;


        UIProgressBar uiProgressBar = self.transform.GetComponent<UIProgressBar>();
        if (uiProgressBar == null)
        {
            uiProgressBar = self.gameObject.AddComponent<UIProgressBar>();

            self.verticalScrollBar = uiProgressBar;

            EventDelegate.Add(uiProgressBar.onChange, self.OnScrollBar);
        }

        uiProgressBar.value = value;
    }

}
