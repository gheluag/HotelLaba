using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp_net8
{
    public class DataBase
    {
        public MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;user=root; password=1234;database=hotel");

        public List<Tables.Rooms> GetRooms()
        {
            List<Tables.Rooms> roomslst = new List<Tables.Rooms>();

            try
            {
                connection.Open();

                string query = "select * from room";

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Tables.Rooms rooms = new Tables.Rooms();

                    rooms.NumRoom = Convert.ToInt32(reader["NUM_ROOM"]);
                    rooms.TypeRoom = reader["type_room"].ToString();
                    rooms.Bed = Convert.ToInt32(reader["BED"]);
                    rooms.Cost = Convert.ToInt32(reader["COST"]);
                    rooms.Status = reader["status"].ToString();
                    rooms.IdKey = Convert.ToInt32(reader["ID_KEY"]);

                    roomslst.Add(rooms);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
            return roomslst;
        }


        public List<Tables.Roomers> GetRoomers()
        {
            List<Tables.Roomers> roomerslst = new List<Tables.Roomers>();

            try
            {
                connection.Open();

                string query = "select ID_ROOMER, SURNAME, FIRSTNAME, PATRONYMIC, PASSPORT, PHONE, NUM_ROOM " +
                    "from roomer " +
                    "join room on NUMROOM = NUM_ROOM";

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Tables.Roomers roomers = new Tables.Roomers();

                    roomers.IdRoomer = Convert.ToInt32(reader["ID_ROOMER"]);
                    roomers.Surname = reader["SURNAME"].ToString();
                    roomers.FirstName = reader["FIRSTNAME"].ToString();
                    roomers.Patronymic = reader["PATRONYMIC"].ToString();
                    roomers.Passport = reader["PASSPORT"].ToString();
                    roomers.Phone = reader["PHONE"].ToString();
                    roomers.RoomNum = Convert.ToInt32(reader["NUM_ROOM"]);

                    roomerslst.Add(roomers);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
            return roomerslst;
        }


        public void AddRoomers(string firstname, string lastname, string patr, string phone, string passport, int numroom)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO roomer (SURNAME, FIRSTNAME, PATRONYMIC, PASSPORT, PHONE, NUMROOM) " +
                       "VALUES (@lastname, @firstname, @patr, @passport, @phone, @numroom)";

                MySqlCommand mySqlCommand = new MySqlCommand(query, connection);

                mySqlCommand.Parameters.AddWithValue("@lastname", lastname);
                mySqlCommand.Parameters.AddWithValue("@firstname", firstname);
                mySqlCommand.Parameters.AddWithValue("@patr", patr);
                mySqlCommand.Parameters.AddWithValue("@passport", passport);
                mySqlCommand.Parameters.AddWithValue("@phone", phone);
                mySqlCommand.Parameters.AddWithValue("@numroom", numroom);

                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Tables.Rooms> GetAvailableRooms()
        {
            List<Tables.Rooms> availableRooms = new List<Tables.Rooms>();

            try
            {
                connection.Open();

                string query = "SELECT * FROM room WHERE status = 'Свободен'";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Tables.Rooms room = new Tables.Rooms
                    {
                        NumRoom = Convert.ToInt32(reader["NUM_ROOM"]),
                        TypeRoom = reader["type_room"].ToString(),
                        Bed = Convert.ToInt32(reader["BED"]),
                        Cost = Convert.ToInt32(reader["COST"]),
                        Status = reader["status"].ToString(),
                        IdKey = Convert.ToInt32(reader["ID_KEY"])
                    };

                    availableRooms.Add(room);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return availableRooms;
        }


        public void UpdateRoomStatus(int roomNum, string newStatus)
        {
            try
            {
                connection.Open();

                string query = "UPDATE room SET status = @newStatus WHERE NUM_ROOM = @roomNum";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@newStatus", newStatus);
                command.Parameters.AddWithValue("@roomNum", roomNum);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public void CheckOutRoomer(int roomerId)
        {
            try
            {
                connection.Open();

                string getRoomNumQuery = "SELECT NUMROOM FROM roomer WHERE ID_ROOMER = @roomerId";
                MySqlCommand getRoomNumCommand = new MySqlCommand(getRoomNumQuery, connection);
                getRoomNumCommand.Parameters.AddWithValue("@roomerId", roomerId);
                int roomNum = Convert.ToInt32(getRoomNumCommand.ExecuteScalar());

                string addJournalEntryQuery = "delete from REG_JOURNAL WHERE ID_ROOMER = @roomerId";
                MySqlCommand addJournalEntryCommand = new MySqlCommand(addJournalEntryQuery, connection);
                addJournalEntryCommand.Parameters.AddWithValue("@roomerId", roomerId);
                addJournalEntryCommand.ExecuteNonQuery();

                string deleteRoomerQuery = "DELETE FROM roomer WHERE ID_ROOMER = @roomerId";
                MySqlCommand deleteRoomerCommand = new MySqlCommand(deleteRoomerQuery, connection);
                deleteRoomerCommand.Parameters.AddWithValue("@roomerId", roomerId);
                deleteRoomerCommand.ExecuteNonQuery();

                string updateRoomStatusQuery = "UPDATE room SET status = 'Свободен' WHERE NUM_ROOM = @roomNum";
                MySqlCommand updateRoomStatusCommand = new MySqlCommand(updateRoomStatusQuery, connection);
                updateRoomStatusCommand.Parameters.AddWithValue("@roomNum", roomNum);
                updateRoomStatusCommand.ExecuteNonQuery();

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
        }


        public List<Tables.Admins> GetAdmins()
        {
            List<Tables.Admins> adminslst = new List<Tables.Admins>();

            try
            {
                connection.Open();

                string query = "select * from ADMIN";

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Tables.Admins admins = new Tables.Admins();

                    admins.Id = Convert.ToInt32(reader["ID_ADMIN"]);
                    admins.Name = reader["FIO_ADMIN"].ToString();
                    admins.IdJournal = Convert.ToInt32(reader["ID_JOURNAL"]);

                    adminslst.Add(admins);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
            return adminslst;
        }


        public List<Tables.RegJournal> GetRegJournal()
        {
            List<Tables.RegJournal> journalEntries = new List<Tables.RegJournal>();

            try
            {
                connection.Open();

                string query = @"
            SELECT 
                REG_JOURNAL.ID_JOURNAL AS Id,
                REG_JOURNAL.REG_TIME AS RegTime,
                REG_JOURNAL.ENTRY AS Entry,
                REG_JOURNAL.DEPARTURE AS Departure,
                roomer.FIRSTNAME AS FirstName,
                roomer.SURNAME AS LastName,
                room.NUM_ROOM AS RoomNum
            FROM REG_JOURNAL
            INNER JOIN roomer ON REG_JOURNAL.ID_ROOMER = roomer.ID_ROOMER
            INNER JOIN room ON REG_JOURNAL.NUM_ROOM = room.NUM_ROOM";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Tables.RegJournal entry = new Tables.RegJournal
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        RegTime = TimeOnly.FromTimeSpan(reader.GetTimeSpan("RegTime")),
                        Entry = DateOnly.FromDateTime(reader.GetDateTime("Entry")),
                        Departure = DateOnly.FromDateTime(reader.GetDateTime("Departure")),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        RoomNum = Convert.ToInt32(reader["RoomNum"])
                    };

                    journalEntries.Add(entry);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return journalEntries;
        }

        public bool IsJournalIdValid(int idJournal)
        {
            try
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM REG_JOURNAL WHERE ID_JOURNAL = @idJournal";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@idJournal", idJournal);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Tables.RegJournal> GetJournalIds()
        {
            List<Tables.RegJournal> journalIds = new List<Tables.RegJournal>();

            try
            {
                connection.Open();

                string query = "SELECT ID_JOURNAL FROM REG_JOURNAL";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    journalIds.Add(new Tables.RegJournal
                    {
                        Id = Convert.ToInt32(reader["ID_JOURNAL"])
                    });
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return journalIds;
        }


        public void AddAdmin(string fioAdmin, int idJournal)
        {
            try
            {
                connection.Open();

                string query = "INSERT INTO ADMIN (FIO_ADMIN, ID_JOURNAL) VALUES (@fioAdmin, @idJournal)";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@fioAdmin", fioAdmin);
                command.Parameters.AddWithValue("@idJournal", idJournal);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public void AddJournalEntry(int idRoomer, int roomNum, DateOnly entryDate, DateOnly departureDate, TimeOnly timeReg)
        {
            try
            {
                connection.Open();

                string query = "INSERT INTO REG_JOURNAL (REG_TIME, ID_ROOMER, NUM_ROOM, ENTRY, DEPARTURE) " +
                               "VALUES (@regTime, @idRoomer, @roomNum, @entryDate, @departureDate)";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@regTime", timeReg);
                command.Parameters.AddWithValue("@idRoomer", idRoomer);
                command.Parameters.AddWithValue("@roomNum", roomNum);
                command.Parameters.AddWithValue("@entryDate", entryDate.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@departureDate", departureDate.ToString("yyyy-MM-dd"));

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public int GetRoomNumberByRoomerId(int idRoomer)
        {
            int roomNumber = 0;

            try
            {
                connection.Open();

                string query = "SELECT NUMROOM FROM roomer WHERE ID_ROOMER = @IdRoomer";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdRoomer", idRoomer);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        roomNumber = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при получении номера комнаты: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return roomNumber;
        }


    }
}
