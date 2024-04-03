using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] private bool[] walls = new bool[4];
    private int[] test_neighbours = new int[4];
    [SerializeField] private int rotation = 0;
    [SerializeField] private float x, z = 0;
    [SerializeField] private bool end_tile = false;
    public int index = 0; 

    public virtual void rotate_tile() {
        rotation = (rotation + 1) % 4;
        bool change_1 = false;
        bool change_2 = false;
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                change_1 = walls[i + 1];
                walls[i + 1] = walls[i];
            }
            else
            {
                change_2 = walls[(i + 1) % 4];
                walls[(i + 1) % 4] = change_1;
                change_1 = change_2;
            }
        }
        gameObject.transform.Rotate(0, 90, 0);
    }
    public void pass_Walls(bool[] wa)
    {
        for (int i = 0; i < 4; i++)
        {
            wa[i] = walls[i];
        }
    }

    public int draw_wall()
    {
        List<int> ints = new List<int>();
        for(int i = 0; i < 4; i++)
        {
            if(walls[i] == true)
            {
                ints.Add(i);
            }
        }
        int ret = Random.Range(0, ints.Count);
        return ints[ret];
    }

    public void set_x_z(float x_point, float z_point)
    {
        x = x_point;
        z = z_point;
    }

    public float get_x()
    {
        return x;
    }

    public float get_z()
    {
        return z;
    }

    public int return_wall_status(int number)
    {
        if(walls[number] == true)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    public void simulate_rotation(bool[] walls_simul)
    {
        bool change_1 = false;
        bool change_2 = false;
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                change_1 = walls_simul[(i + 1) % 4];
                walls_simul[(i + 1) % 4] = walls_simul[i];
            }
            else
            {
                change_2 = walls_simul[(i + 1) % 4];
                walls_simul[(i + 1) % 4] = change_1;
                change_1 = change_2;
            }
        }
    }
    public bool check_corectness(int[] neighbours, bool only_check_once)
    {
        bool[] walls_simul = new bool[4];
        for(int i = 0; i < 4; i++)
        {
            walls_simul[i] = walls[i];
        }
        bool[] t = new bool[neighbours.Length];

        for(int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (neighbours[j] != 3)
                {
                    if (neighbours[j] == 1 && walls_simul[j] == true || neighbours[j] == 0 && walls_simul[j] == false)
                    {
                        t[j] = true;
                    }
                    else
                    {
                        t[j] = false;
                    }
                }
                else
                {
                    t[j] = true;
                }
            }

            if (t[0] && t[1] && t[2] && t[3])
            {
                return true;
            }
            else
            {
                if(only_check_once == true)
                {
                    return false;
                }
                else
                {
                    simulate_rotation(walls_simul);
                }
                
            }
        }
        return false;
    }
    public void set_end_tile()
    {
        end_tile = true;
    }
    
    public bool get_end_tile()
    {
        return end_tile;
    }

    
    public int get_num_paths()
    {
        int num = 0;
        for(int i = 0; i < 4; i++)
        {
            if (walls[i] == true)
            {
                num++;
            }
        }
        return num;
    }

    public void set_test_neighbours(int w, int nei)
    {
        test_neighbours[w] = nei;
    }
    public void set_index(int i)
    {
        index = i;
    }
    public int ret_index()
    {
        return index;
    }
}
