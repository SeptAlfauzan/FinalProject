
public class CollectibleItem//keep multi item (inventory used)
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
    public CollectibleItem Clone(){
        return new CollectibleItem(this.length, this.itemData);
    }
}