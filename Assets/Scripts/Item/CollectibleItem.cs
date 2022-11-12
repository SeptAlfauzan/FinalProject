
public class CollectibleItem
{
    int length;
    ItemData itemData;
    public CollectibleItem(int length, ItemData itemData){
        this.length = length;
        this.itemData = itemData;
    }
    public int GetLength(){
        return length;
    }
    public void SetLength(int num){
        length = num;
    }
    public ItemData GetItemData(){
        return itemData;
    }
    public void SetItemData(ItemData data){
        itemData = data;
    }
}