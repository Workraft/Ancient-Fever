using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MapCreation : MonoBehaviour
{
    private GameObject local;
    private float move = 6.0f;
    public GameObject[] Tile_Spawner = new GameObject[8];
    public GameObject totem_tile;

    public int Number_of_minigames_min;
    public int Number_of_minigames_max;

    public int Number_of_tiles_min;
    public int Number_of_tiles_max;

    private float x ,z = 0.0f;

    public static int num_of_tiles_on_map = 0;

    public int num_of_non_game_corri = 3;
    public int num_of_liana = 0;

    private List<GameObject> Tiles_on_map = new List<GameObject>();

    void check_neighbor_of_tile(int[] neighbors)
    {
        // 1 - korytarz, 0 - œciana, 3 - brak s¹siada
        for (int i = 0; i < 4; i++)
        {
            neighbors[i] = 3;
        }
        for (int i = 0; i < Tiles_on_map.Count; i++)
        {
            if (Tiles_on_map[i].GetComponent<TileScript>().get_x() == x && Tiles_on_map[i].GetComponent<TileScript>().get_z() == z - move)
            {
                neighbors[0] = Tiles_on_map[i].GetComponent<TileScript>().return_wall_status(2);
            }
            if (Tiles_on_map[i].GetComponent<TileScript>().get_x() == x - move && Tiles_on_map[i].GetComponent<TileScript>().get_z() == z)
            {
                neighbors[1] = Tiles_on_map[i].GetComponent<TileScript>().return_wall_status(3);
            }
            if (Tiles_on_map[i].GetComponent<TileScript>().get_x() == x && Tiles_on_map[i].GetComponent<TileScript>().get_z() == z + move)
            {
                neighbors[2] = Tiles_on_map[i].GetComponent<TileScript>().return_wall_status(0);
            }
            if (Tiles_on_map[i].GetComponent<TileScript>().get_x() == x + move && Tiles_on_map[i].GetComponent<TileScript>().get_z() == z)
            {
                neighbors[3] = Tiles_on_map[i].GetComponent<TileScript>().return_wall_status(1);
            }
        }
    }

    bool chec_correct(bool[] wall_sim, int[] neighbours)
    {
        bool[] t = new bool[neighbours.Length];

            for (int j = 0; j < 4; j++)
            {
                if (neighbours[j] != 3)
                {
                    if (neighbours[j] == 1 && wall_sim[j] == true || neighbours[j] == 0 && wall_sim[j] == false)
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

                return false;
            }
    }
    bool if_is_good_rotation(GameObject tile, int[] neighbors, int wal, int last_move)
    {
        bool[] wall_sim = new bool[4];
        tile.GetComponent<TileScript>().pass_Walls(wall_sim);
        switch (last_move)
        {   
            case 1: 
                if (wal != 0)
                {
                    for (int i = 0; i < (4 - wal) % 4; i++)
                    {
                        tile.GetComponent<TileScript>().simulate_rotation(wall_sim);
                    }
                }
                if(chec_correct(wall_sim, neighbors) == false)
                {
                    return false;
                }
                break;

            case -1:
                if (wal != 2)
                {
                    for (int i = 0; i < (4 - wal + 2) % 4; i++)
                    {
                        tile.GetComponent<TileScript>().simulate_rotation(wall_sim);
                    }
                }
                if (chec_correct(wall_sim, neighbors) == false)
                {
                    return false;
                }
                break;

            case 2:
                if (wal != 3)
                {
                    for (int i = 0; i < (4 - wal + 3) % 4; i++)
                    {
                        tile.GetComponent<TileScript>().simulate_rotation(wall_sim);
                    }
                }
                if (chec_correct(wall_sim, neighbors) == false)
                {
                    return false;
                }
                break;

            case -2:
                if (wal != 1)
                {
                    for (int i = 0; i < (4 - wal + 1) % 4; i++)
                    {
                        tile.GetComponent<TileScript>().simulate_rotation(wall_sim);
                    }
                }
                if (chec_correct(wall_sim, neighbors) == false)
                {
                    return false;
                }
                break;
        }

        return true;
    }
    void do_a_good_rotation(GameObject tile, int last_move)
    {
        bool[] wall = new bool[4];
        tile.GetComponent<TileScript>().pass_Walls(wall);

        int[] neighbors = new int[4];
        check_neighbor_of_tile(neighbors);

        List<int> ints = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            if (wall[i] == true)
            {
                ints.Add(i);
            }
        }
        int wall_num2 = 0;
        bool end = true;

        int zabezpieczenie = 0;

        while (end)
        {
            wall_num2 = Random.Range(0, ints.Count);
            switch (last_move)
            {
                case 1: //droga od do³u, wall number = 0 (lub 4)
                    if(ints.Count == 1 || if_is_good_rotation(tile, neighbors, ints[wall_num2], last_move) == true)
                    {
                        if (ints[wall_num2] != 0)
                        {
                            for (int i = 0; i < (4 - ints[wall_num2]) % 4; i++)
                            {
                                tile.GetComponent<TileScript>().rotate_tile();
                            }
                        }
                        end = false;
                    }
                    else
                    {
                        ints.Remove(ints[wall_num2]);
                    } 
                    break;
                case -1://droga od góry, wall number = 2
                    if (ints.Count == 1 || if_is_good_rotation(tile, neighbors, ints[wall_num2], last_move) == true)
                    {

                        if (ints[wall_num2] != 2)
                        {
                            for (int i = 0; i < (4 - ints[wall_num2] + 2) % 4; i++)
                            {
                                tile.GetComponent<TileScript>().rotate_tile();
                            }
                        }
                        end = false;
                    }
                    else
                    {
                        ints.Remove(ints[wall_num2]);
                    }
                    break;
                case 2://droga od prawej, wall number = 3
                    if (ints.Count == 1 || if_is_good_rotation(tile, neighbors, ints[wall_num2], last_move) == true)
                    {
                        if (ints[wall_num2] != 1)
                        {
                            for (int i = 0; i < (4 - ints[wall_num2] + 1) % 4; i++)
                            {
                                tile.GetComponent<TileScript>().rotate_tile();
                            }
                        }
                        end = false;
                    }
                    else
                    {
                        ints.Remove(ints[wall_num2]);
                    }
                    break;
                case -2://droga od lewej, wall number = 1
                    if (ints.Count == 1 || if_is_good_rotation(tile, neighbors, ints[wall_num2], last_move) == true)
                    {
                        if (ints[wall_num2] != 3)
                        {
                            for (int i = 0; i < (4 - ints[wall_num2] + 3) % 4; i++)
                            {
                                tile.GetComponent<TileScript>().rotate_tile();
                            }
                        }
                        end = false;
                    }
                    else
                    {
                        ints.Remove(ints[wall_num2]);
                    }
                    break;
                default:
                    //Debug.Log("ERROR do_a_good_rotation");
                    //Debug.Log(Tiles_on_map.Count);
                    //Debug.LogError(Tiles_on_map.Count);
                    end = false;
                    break;
            }

            zabezpieczenie++;

            if (zabezpieczenie == 30)
            {
                //Debug.Log("Zabezbieczenie do_a_good_rotation");
                end = false;
            }
        }
    }
    bool check_tile()
    {
        for (int i = 0; i < Tiles_on_map.Count; i++)
        {
           if (Tiles_on_map[i].GetComponent<TileScript>().get_x() == x && Tiles_on_map[i].GetComponent<TileScript>().get_z() == z)
           {
                return false;      
           }
        }
        return true;
    }
    int Choose_path(GameObject tile)
    {
        // -1 = z - move
        // 1 = z + move
        // -2 = x - move
        // 2 = x + move

        int location_change = 0;
        bool[] wall = new bool[4];
        tile.GetComponent<TileScript>().pass_Walls(wall);
        List<int> ints = new List<int>();

        for(int i = 0; i < 4; i++)
        {
            if (wall[i] == true)
            {
                ints.Add(i);
            }
        }
        bool losoj = true;
        int zabezpieczenie = 0;
        
        while (losoj)
        {  
            if(ints.Count != 0)
            {
                int wall_num2 = Random.Range(0, ints.Count);

                switch (ints[wall_num2])
                {
                    case 0:
                        z -= move;
                        if( check_tile() == false)
                        {
                            z += move;
                            ints.Remove(ints[wall_num2]);
                            break;
                        }
                        location_change = -1;
                        losoj = false;
                        break;
                    case 1:
                        x -= move;
                        if (check_tile() == false)
                        {
                            x += move;
                            ints.Remove(ints[wall_num2]);
                            break;
                        }
                        location_change = -2;
                        losoj = false;
                        break;
                    case 2:
                        z += move;
                        if (check_tile() == false)
                        {
                            z -= move;
                            ints.Remove(ints[wall_num2]);
                            break;
                        }
                        location_change = 1;
                        losoj = false;
                        break;
                    case 3:
                        x += move;
                        if (check_tile() == false)
                        {
                            x -= move;
                            ints.Remove(ints[wall_num2]);
                            break;
                        }
                        location_change = 2;
                        losoj = false;
                        break;
                    default:
                        //Debug.LogError("default w choose path");
                        losoj = false;
                        break;
                }
                
            }
            else
            {
                //Debug.Log(Tiles_on_map.Count);
                //Debug.LogError("Error");
                losoj = false;
            }
            
            zabezpieczenie++;
            
            if (zabezpieczenie == 30)
            {
                //Debug.Log("Zabezbieczenie Chose_path");
                losoj = false;
            }
            
        }
        return location_change;
    }
    int find_good_tile(int num_of_corri, int[] neighbors, bool limit, int limit_counter)
    {
        // 1 - korytarz, 0 - œciana, 3 - brak s¹siada
        List<int> tiles = new List<int>();

        for (int i = 0; i < 4; i++) // -2 oznacza nie losowanie koñcowego i œlepego
        {
            if (Tile_Spawner[i].GetComponent<TileScript>().check_corectness(neighbors, false) == true)
            {
                tiles.Add(i);
            }
        }
        int random = 0;

        // 7 - minigame
        if (limit == false)
        {
            if(tiles.Count != 0)
            {
                if (num_of_corri < 1)
                {
                    if (tiles.Contains(0) && tiles.Contains(1))
                    {
                        random = Random.Range(0, 1);
                        if(tiles[random] == 0)
                        {
                            if (num_of_non_game_corri >= 3)
                            {
                                num_of_non_game_corri = 0;
                                return 7;
                            }
                            num_of_non_game_corri++;
                            return 0;
                        }
                        return tiles[random];
                    }
                    else
                    {
                        if (tiles.Contains(0)) { 
                            if(num_of_non_game_corri >= 3)
                            {
                                num_of_non_game_corri = 0;
                                return 7;
                            }
                            num_of_non_game_corri ++;
                            return 0; 
                        }
                        else { return 1; }
                    }
                }
                else
                {
                    random = Random.Range(0, tiles.Count);
                    if (tiles[random] == 0)
                    {
                        if (num_of_non_game_corri >= 3)
                        {
                            num_of_non_game_corri = 0;
                            return 7;
                        }
                        num_of_non_game_corri++;
                        return tiles[random];
                    }
                    return tiles[random];
                }
            }
            else
            {
                return 4;
            }
               
        }
        else // jest limit
        {
            if( limit_counter != 0 && tiles.Count != 0)
            {
                tiles.Add(4);
                random = Random.Range(0, tiles.Count);
                if (tiles[random] == 0)
                {
                    if (num_of_non_game_corri >= 3)
                    {
                        num_of_non_game_corri = 0;
                        return 7;
                    }
                    num_of_non_game_corri++;
                    return tiles[random];
                }
                return tiles[random];
            }
            else
            {
                return 4;
            }
        }
       
        //return 4;
    }
    int pass_the_tile(int num_of_corri, int num_of_noncorri)
    {
        //int tile_num = 0;
        int[] neighbors = new int[4];
        //int neighborhood = 0;
        if (Tiles_on_map.Count == 0)
        {
            return 6; // Tile pocz¹tkowy
        }
        else
        {
            check_neighbor_of_tile(neighbors);
            
            return find_good_tile(num_of_corri,neighbors, false, 0);
        }

        //return tile_num;
    }

    bool possible_path(GameObject tile, int[] nei)
    {
        /* 
         * prawda, je¿eli ma mo¿liw¹ œcie¿kê
         * Mo¿liwa œcie¿ka jest gdy tile ma œcie¿kê (wall == true) i s¹siadem jest pusta przestrzeñ (czyli 3)
         */
        bool[] wa = new bool[4];
        tile.GetComponent<TileScript>().pass_Walls(wa);
        for(int i = 0; i < 4; i++)
        {
            if (wa[i]==true && nei[i] == 3)
            {
                return true;
            }
        }

        return false;
    }

    bool if_no_free_path(GameObject tile)
    {
        int[] neighbors = new int[4];
        check_neighbor_of_tile(neighbors);

        return possible_path(tile, neighbors);
    }
    void create_rest_paths(int max_new_tiles)
    {
        bool end = true;
        int current_tile = 0; // Tile startowy
        int last_move = 0;
        int last_move_2 = 0;
        int tile_num = 0;
        int num_of_corri = 0;
        int[] neighbors = new int[4];

        int zabezpiecz = 0;

        int licznik = max_new_tiles;
        bool licznik_start = false;
        bool lec_dalej;


        while (end) // Tak d³ugo jak jest niedokoñczony korytarz to rób œcie¿ki. 
        {
            x = Tiles_on_map[current_tile].GetComponent<TileScript>().get_x();
            z = Tiles_on_map[current_tile].GetComponent<TileScript>().get_z();
            check_neighbor_of_tile(neighbors);
            lec_dalej = possible_path(Tiles_on_map[current_tile], neighbors);

            if (lec_dalej == true && 
               Tiles_on_map[current_tile].GetComponent<TileScript>().get_end_tile() == false)// je¿eli Tile na którym jesteœmy ma wolny korytarz i nie jest ostatnim tilem
            {   

                last_move = Choose_path(Tiles_on_map[current_tile]);
                
                if(last_move != 0)
                {
                    for (int i = licznik; i >= 0; i--)
                    {
                        check_neighbor_of_tile(neighbors);
                        tile_num = find_good_tile(1, neighbors, true, i);

                        if (tile_num < 2) { num_of_corri++; }
                        else { num_of_corri = 0; }

                        local = Instantiate(Tile_Spawner[tile_num], new Vector3(0 + x, 0, 0 + z), Quaternion.identity);
                        local.GetComponent<TileScript>().set_x_z(x, z);
                        if (tile_num == 7)
                        {
                            local.GetComponent<TileScript>().set_index(num_of_liana);
                            num_of_liana++;
                        }
                        do_a_good_rotation(local, last_move);
                        Tiles_on_map.Add(local);
                        if (tile_num == 4)
                        {
                            if (if_no_free_path(Tiles_on_map[current_tile]) == false)
                            {
                                current_tile++;
                            }
                            break;
                        }
                        last_move_2 = last_move;
                        last_move = Choose_path(local); 
                        if(last_move == 0)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    //Debug.Log("Problem");
                    current_tile++; 
                }

                
            }
            else
            {
                if (Tiles_on_map[current_tile].GetComponent<TileScript>().get_end_tile() == true)
                {
                    licznik_start = true;
                }
                current_tile++;
            }
            if(licznik_start == true && licznik > 0)
            {
                licznik--;
            }
            
            if (current_tile >= Tiles_on_map.Count)
            {

                //Debug.Log("Zakonczylo sie poprawnie");
                end = false;
                break;
            }
            zabezpiecz++;
            if(zabezpiecz == 400)
            {
                //Debug.Log(current_tile);
                //Debug.Log(lec_dalej);
                //Debug.Log(x);
                //Debug.Log(z);
                //Debug.Log("Zabezpieczenie zadzialalo");
                end = false;
                break;
            }
        }
        for(int i = 0; i < Tiles_on_map.Count; i++)
        {
            if (Tiles_on_map[i].GetComponent<TileScript>().get_end_tile() == true)
            {
                GameObject end_tile = Tiles_on_map[i];
                x = end_tile.GetComponent<TileScript>().get_x();
                z = end_tile.GetComponent<TileScript>().get_z();
                check_neighbor_of_tile(neighbors);
                for( int j = 0; j < 4; j++)
                {
                    x = end_tile.GetComponent<TileScript>().get_x();
                    z = end_tile.GetComponent<TileScript>().get_z();
                    if (neighbors[j] == 3)
                    {
                        last_move = Choose_path(end_tile);
                        local = Instantiate(Tile_Spawner[4], new Vector3(0 + x, 0, 0 + z), Quaternion.identity);
                        local.GetComponent<TileScript>().set_x_z(x, z);
                        do_a_good_rotation(local, last_move);
                        Tiles_on_map.Add(local);
                    }
                }

                break;
            }
        }
    }
    void initiate_map_creation()
    {
        float Num_of_games = Random.Range(Number_of_minigames_min, Number_of_minigames_max);
        float Num_of_tiles = Random.Range(Number_of_tiles_min, Number_of_tiles_max);
        float sum_of_tiles = Num_of_games + Num_of_tiles;
        int tile_num = 0;
        int num_of_corri = 0;
        int num_of_noncorri = 0;
        int last_move = 0;
        int last_move_2 = last_move;
        for (int i = 0; i < (int)sum_of_tiles; i++) 
        {
            tile_num = pass_the_tile(num_of_corri, num_of_noncorri);

            if(tile_num < 2) {num_of_corri++;}
            else {num_of_corri = 0;}
            
            local = Instantiate(Tile_Spawner[tile_num], new Vector3(0 + x, 0, 0 + z), Quaternion.identity);
            local.GetComponent<TileScript>().set_x_z(x, z);
            if(tile_num == 7)
            {
                local.GetComponent<TileScript>().set_index(num_of_liana);
                num_of_liana++;
            }
            if (i != 0)
            {
                do_a_good_rotation(local, last_move);
            }
            Tiles_on_map.Add(local);

            last_move_2 = last_move;

            if(if_no_free_path(local) == true) { 
                last_move = Choose_path(local); 
            }
            else
            {
                last_move = 0;
            }
            
            if(last_move == 0)
            {
                for(int j = i-1; j >= 0; j--)
                {
                    local = Tiles_on_map[j];
                    x = local.GetComponent<TileScript>().get_x(); // ustaw x
                    z = local.GetComponent<TileScript>().get_z(); // ustaw z
                    if (if_no_free_path(local) == true)
                    {
                        last_move = Choose_path(local);
                        i = j;
                        break;
                    }
                }
            }
        }
        local = Instantiate(Tile_Spawner[5], new Vector3(0 + x, 0, 0 + z), Quaternion.identity);
        local.GetComponent<TileScript>().set_x_z(x, z);
        local.GetComponent<TileScript>().set_end_tile();
        Tiles_on_map.Add(local);

        totem_tile = local;

        create_rest_paths(4);
        num_of_tiles_on_map = Tiles_on_map.Count;
    }

    bool spawn_too_close()
    {
        float x = totem_tile.GetComponent<TileScript>().get_x();
        float z = totem_tile.GetComponent<TileScript>().get_z();

        if ( x <= 18 && x >= -18)
        {
            if(z <= 18 && z >= -18)
            {
                //Debug.LogError("B³¹d mapy: Totem za blisko Spawnu");
                return true;
            }
        }
        return false;
    }
    bool tile_has_empty_path()
    {
        for(int i = 0; i < Tiles_on_map.Count; i++)
        {
            x = Tiles_on_map[i].GetComponent<TileScript>().get_x();
            z = Tiles_on_map[i].GetComponent<TileScript>().get_z();
            if (if_no_free_path(Tiles_on_map[i]) == true)
            {
                //Debug.LogError("B³¹d mapy: Tile ma pust¹ przestrzeñ");
                return true;
            }
        }
        return false;
    }
    bool map_created_good()
    {
        if(spawn_too_close() == true)
        {
            return false;
        }
        if(tile_has_empty_path()== true)
        {
            return false;
        }
        return true;
    }
    void Start()
    {
        initiate_map_creation();
        
        if(map_created_good() == false)
        {
            //Debug.LogError("Inicjuje tworzenie nowej mapy");
            SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);
        }
        
    }
    public static int ret_num_tiles()
    {
        return num_of_tiles_on_map;
    }
}





