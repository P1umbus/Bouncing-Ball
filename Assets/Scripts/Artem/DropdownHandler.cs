using UnityEngine;

public class DropdownHandler : MonoBehaviour
{
    [SerializeField] private ItemManager[] _itemManagers;

    public void Sort(int value)
    {
        foreach (ItemManager item in _itemManagers)
        {
            if (value == 0)
                item.gameObject.SetActive(true);
            else if (item.Rarity == value - 1)
                item.gameObject.SetActive(true);
            else
                item.gameObject.SetActive(false);
        }
    }
}
